﻿using System;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1EasyEcs.Scripts.Custom;
using Exerussus._1Extensions.Scripts.Extensions;
using Exerussus._1Extensions.SignalSystem;
using Exerussus._1Lab.Scripts.ECS.Systems;
using Exerussus._1Lab.Scripts.Data.GamesConfigurations;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public class OneLab : EcsStarter
    {
        #region Fields And Initializing

        private static OneLab _instance;
        private static bool _isInitialized;
        public static Componenter Componenter => Instance._componenter;
        public static EcsWorld World => Instance._world;
        public static Signal Signal => Instance._signalHandler.Signal;
        private event Action OnDestroyEvent;
        private OneLabConfiguration _configuration;
        private SignalHandler _signalHandler;
        private TagsHandler _tagsHandler = new();
        private OneLabPooler _oneLabPooler;
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
                    _instance._configuration = _instance.TrySetDataIfNull(ref _instance._configuration);
                    _instance._signalHandler = _instance.TrySetDataIfNull(ref _instance._signalHandler);
                    
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
            initSystems
                
                .Add(new TagSystem());
            
            ExtraSystemsMethods.InitExecute(initSystems);
        }

        protected override void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems)
        {
            fixedUpdateSystems
                    
                .Add(new RotationSystem())
                .Add(new FlipSystem())
                .Add(new SpeedLimitSystem())
                .Add(new AlphaColorSystem())
                .Add(new VfxSystem())
                .Add(new CharacterAnimatorSystem())
                .Add(new GlobalTriggerSystem())
                .Add(new KeyColliderSwitcherSystem())
                .Add(new SorterSystem())
                .Add(new DestroySystem());
            
            ExtraSystemsMethods.FixedUpdateExecute(fixedUpdateSystems);
        }

        protected override void SetUpdateSystems(IEcsSystems updateSystems)
        {
            updateSystems
                    
                .Add(new InputMoverSystem())
                .Add(new MoveSystem())
                .Add(new TriggerSystem());
            
            ExtraSystemsMethods.UpdateExecute(updateSystems);
        }

        protected override void SetLateUpdateSystems(IEcsSystems lateUpdateSystems)
        {
            lateUpdateSystems
                
                .Add(new CameraSystem());
        }

        protected override void SetTickUpdateSystems(IEcsSystems tickUpdateSystems)
        {
            ExtraSystemsMethods.TickExecute(tickUpdateSystems);
        }

        protected override void SetSharingData(EcsWorld world, GameShare gameShare)
        {
            _oneLabPooler = new OneLabPooler(world);
            GameShare.AddSharedObject(_oneLabPooler);
            GameShare.AddSharedObject(_configuration);
            GameShare.AddSharedObject(_tagsHandler);
            GameShare.AddSharedObject(_signalHandler.Signal);
        }

        protected override Signal GetSignal()
        {
            return _signalHandler.Signal;
        }
    }

    public interface IOneLabEcsData : IEcsComponent
    {
        
    }
}
