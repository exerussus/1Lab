using System;
using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SignalSystem;
using Leopotam.EcsLite;
using Exerussus._1Lab.Scripts.ECS.Systems;
using Exerussus._1Lab.Scripts.Data.GamesConfigurations;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public class OneLab : OneLabStarter
    {
        #region Fields And Initializing

        private static OneLab _instance;
        private static bool _isInitialized;
        private static Dictionary<Type, DataPack> _sharedData;
        private static GameShare _gameShare;
        public static Componenter Componenter => Instance._componenter;
        public static EcsWorld World => Instance._world;
        public static Signal Signal => Instance._configuration.Signal;
        private event Action OnDestroyEvent;
        private OneLabConfiguration _configuration;
        public static OneLabConfiguration Configuration => Instance._configuration;

        private static OneLab Instance
        {
            get
            {
                if (!_isInitialized)
                {
                    _isInitialized = true;
                    _instance = new GameObject().AddComponent<OneLab>();
                    _instance.gameObject.name = "OneLab";
                    _instance.OnDestroyEvent += () => _isInitialized = false;
                    _instance._configuration = Resources.Load<OneLabConfiguration>("OneLabConfiguration");
                    _sharedData = new Dictionary<Type, DataPack>();
                    _gameShare = new GameShare(_sharedData);
                    _gameShare.AddSharedObject(_instance._configuration.GetType(), _instance._configuration);
                    _gameShare.AddSharedObject(_instance._configuration.Signal.GetType(), _instance._configuration.Signal);
                    
                    _instance.PreInit(_gameShare);
                    _instance.Initialize();
                }

                return _instance;
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnDestroyEvent?.Invoke();
        }

        #endregion
        
        protected override void SetInitSystems(IEcsSystems initSystems)
        {
            ExtraSystemsMethods.InitExecute(initSystems);
        }

        protected override void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems)
        {
            fixedUpdateSystems.Add(new RotationSystem());
            fixedUpdateSystems.Add(new MoveSystem());
            fixedUpdateSystems.Add(new FlipSystem());
            fixedUpdateSystems.Add(new SpeedLimitSystem());
            fixedUpdateSystems.Add(new AlphaColorSystem());
            fixedUpdateSystems.Add(new VfxSystem());
            fixedUpdateSystems.Add(new CharacterAnimatorSystem());
            fixedUpdateSystems.Add(new GlobalTriggerSystem());
            fixedUpdateSystems.Add(new KeyColliderSwitcherSystem());
            ExtraSystemsMethods.FixedUpdateExecute(fixedUpdateSystems);
        }

        protected override void SetUpdateSystems(IEcsSystems updateSystems)
        {
            updateSystems.Add(new InputMoverSystem());
            updateSystems.Add(new TriggerSystem());
            ExtraSystemsMethods.UpdateExecute(updateSystems);
        }

        protected override void SetTickUpdateSystems(IEcsSystems tickUpdateSystems)
        {
            ExtraSystemsMethods.TickExecute(tickUpdateSystems);
        }
    }
}
