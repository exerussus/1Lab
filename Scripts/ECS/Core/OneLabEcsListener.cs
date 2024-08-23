using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.Data.GamesConfigurations;
using Leopotam.EcsLite;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public abstract class OneLabEcsListener<T1> : EcsSignalListener<OneLabPooler, T1>
        where T1 : struct
    {
        protected OneLabConfiguration Configuration;
        protected TagsHandler TagsHandler;
        
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            gameShare.GetSharedObject(ref Configuration);
            gameShare.GetSharedObject(ref TagsHandler);
        }
    }
    
    public abstract class OneLabEcsListener<T1, T2> : EcsSignalListener<OneLabPooler, T1, T2> 
        where T1 : struct 
        where T2 : struct 
    {
        protected OneLabConfiguration Configuration;
        protected TagsHandler TagsHandler;
        
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            gameShare.GetSharedObject(ref Configuration);
            gameShare.GetSharedObject(ref TagsHandler);
        }
    }

    public abstract class OneLabEcsListener<T1, T2, T3> : EcsSignalListener<OneLabPooler, T1, T2, T3>
        where T1 : struct
        where T2 : struct
        where T3 : struct
    {
        protected OneLabConfiguration Configuration;
        protected TagsHandler TagsHandler;
        
        public override void PreInit(GameShare gameShare, float tickTime,EcsWorld world, 
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            gameShare.GetSharedObject(ref Configuration);
            gameShare.GetSharedObject(ref TagsHandler);
        }
    }

    public abstract class OneLabEcsListener<T1, T2, T3, T4> : EcsSignalListener<OneLabPooler, T1, T2, T3, T4>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
    {
        protected OneLabConfiguration Configuration;
        protected TagsHandler TagsHandler;
        
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            gameShare.GetSharedObject(ref Configuration);
            gameShare.GetSharedObject(ref TagsHandler);
        }
    }

    public abstract class OneLabEcsListener<T1, T2, T3, T4, T5> : EcsSignalListener<OneLabPooler, T1, T2, T3, T4, T5>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
    {
        protected OneLabConfiguration Configuration;
        protected TagsHandler TagsHandler;
        
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            gameShare.GetSharedObject(ref Configuration);
            gameShare.GetSharedObject(ref TagsHandler);
        }
    }

    public abstract class OneLabEcsListener<T1, T2, T3, T4, T5, T6> : EcsSignalListener<OneLabPooler, T1, T2, T3, T4, T5, T6>
        where T1 : struct
        where T2 : struct
        where T3 : struct
        where T4 : struct
        where T5 : struct
        where T6 : struct
    {
        protected OneLabConfiguration Configuration;
        protected TagsHandler TagsHandler;
        
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world,
            InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            gameShare.GetSharedObject(ref Configuration);
            gameShare.GetSharedObject(ref TagsHandler);
        }
    }
}