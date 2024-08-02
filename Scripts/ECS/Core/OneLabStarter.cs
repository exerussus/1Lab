
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.Data.GamesConfigurations;
using Exerussus._1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS
{
    public abstract class OneLabStarter : BootstrapComponent
    {
        protected EcsWorld _world;
        protected Componenter _componenter;
        protected IEcsSystems _stepByStepSystems;
        protected IEcsSystems _cardViewRefreshSystems;
        protected IEcsSystems _coreSystems;
        protected IEcsSystems _initSystems;
        protected IEcsSystems _fixedUpdateSystems;
        protected IEcsSystems _updateSystems;
        protected IEcsSystems _tickUpdateSystems;
        protected float _tickTimer;
        private OneLabConfiguration _oneLabConfiguration;
        
        protected override void OnPreInit()
        {
            _world = new EcsWorld();
            _componenter = new Componenter(_world);
            GameShare.AddSharedObject(_componenter.GetType(), _componenter);
            _oneLabConfiguration = GameShare.GetSharedObject<OneLabConfiguration>();
            
            PrepareCoreSystems();
            PrepareInitSystems();
            PrepareFixedUpdateSystems();
            PrepareUpdateSystems();
            PrepareTickUpdateSystems();
            DependencyInject();
        }

        private void DependencyInject()
        {
            InjectSystems(_coreSystems);
            InjectSystems(_initSystems);
            InjectSystems(_fixedUpdateSystems, InitializeType.FixedUpdate);
            InjectSystems(_updateSystems, InitializeType.Update);
            InjectSystems(_tickUpdateSystems, InitializeType.Tick);
        }
        
        private void InjectSystems(IEcsSystems systems, InitializeType initializeType = InitializeType.None)
        {
            foreach (var system in systems.GetAllSystems())
            {
                if (system is EasySystem easySystem)
                {
                    easySystem.PreInit(GameShare, _oneLabConfiguration.TickDelay, initializeType);
                }
            }
        }
        
        public override void Initialize()
        {
            _coreSystems.Init();
            _initSystems.Init();
            _fixedUpdateSystems.Init();
            _updateSystems.Init();
            _tickUpdateSystems.Init();
        }
        
        protected abstract void SetInitSystems(IEcsSystems initSystems);
        protected abstract void SetFixedUpdateSystems(IEcsSystems fixedUpdateSystems);
        protected abstract void SetUpdateSystems(IEcsSystems updateSystems);
        protected abstract void SetTickUpdateSystems(IEcsSystems tickUpdateSystems);

        private void TryInvokeTick()
        {
            _tickTimer += Time.fixedDeltaTime;
            if (!(_tickTimer >= _oneLabConfiguration.TickDelay)) return;
            _tickTimer -= _oneLabConfiguration.TickDelay;
            _tickUpdateSystems?.Run();
        }
        
        private void PrepareCoreSystems()
        {
            _coreSystems = new EcsSystems(_world, GameShare);
        }
        
        private void PrepareInitSystems()
        {
            _initSystems = new EcsSystems(_world, GameShare);
            SetInitSystems(_initSystems);
        }
        
        private void PrepareFixedUpdateSystems()
        {
            _fixedUpdateSystems = new EcsSystems(_world, GameShare);
            SetFixedUpdateSystems(_fixedUpdateSystems);
            _fixedUpdateSystems.Add(new DestroySystem());
        }
        
        private void PrepareUpdateSystems()
        {
            _updateSystems = new EcsSystems(_world, GameShare);
            SetUpdateSystems(_updateSystems);
        }
        
        private void PrepareTickUpdateSystems()
        {
            _tickUpdateSystems = new EcsSystems(_world, GameShare);
            SetTickUpdateSystems(_tickUpdateSystems);
        }
        
        protected virtual void OnDestroy() 
        {
            _coreSystems?.Destroy();
            _initSystems?.Destroy();
            _fixedUpdateSystems?.Destroy();
            _tickUpdateSystems?.Destroy();
        }

        public void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
            TryInvokeTick();
        }

        public void Update()
        {
            _updateSystems?.Run();
        }
    }
}
