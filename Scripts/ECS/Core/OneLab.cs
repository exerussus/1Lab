using System;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1Extensions.SignalSystem;
using Exerussus._1Lab.Scripts.ECS.Systems;
using Exerussus._1Lab.Scripts.Data.GamesConfigurations;
using Plugins.Exerussus._1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public class OneLab : EcsStarter
    {
        #region Fields And Initializing

        private static OneLab _instance;
        private static bool _isInitialized;
        private static GameShare _gameShare;
        public static Componenter Componenter => Instance._componenter;
        public static EcsWorld World => Instance._world;
        public static Signal Signal => Instance._configuration.Signal;
        private event Action OnDestroyEvent;
        private OneLabConfiguration _configuration;
        private TagsHandler _tagsHandler = new();
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
                    
                    _gameShare.AddSharedObject(_instance._configuration);
                    _gameShare.AddSharedObject(_instance._tagsHandler);
                    
                    _instance.PreInitialize();
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
            initSystems.Add(new TagSystem());
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

        protected override void SetSharingData(GameShare gameShare)
        {
            _configuration = Resources.Load<OneLabConfiguration>("OneLabConfiguration");
            _gameShare.AddSharedObject(_configuration);
            _gameShare.AddSharedObject(_tagsHandler);
            _gameShare.AddSharedObject(_configuration.Signal);
        }

        protected override Signal GetSignal()
        {
            return _configuration.Signal;
        }
    }
}
