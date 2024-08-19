using Exerussus._1Extensions.SignalSystem;
using Exerussus._1Lab.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Filters;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public static class OneLabSignals
    {
        
        public struct CommandSpawnObject : IOneLabSignal
        {
            
        }
        
        /// <summary>
        /// Команда на уничтожение Entity 
        /// </summary>
        public struct CommandKillEntitySignal : IOneLabSignal
        {
            public int Entity;
            public bool Immediately;
        }
    
        /// <summary>
        /// MonoBehaviourView проинициализировался.
        /// </summary>
        public struct OnEcsMonoBehaviorInitializedSignal : IOneLabSignal
        {
            public IEcsMonoBehavior EcsMonoBehavior;
        }
    
        /// <summary>
        /// MonoBehaviourView уничтожился.
        /// </summary>
        public struct OnEcsMonoBehaviorStartDestroySignal : IOneLabSignal
        {
            public IEcsMonoBehavior EcsMonoBehavior;
        }
    
        /// <summary>
        /// MonoBehaviourView уничтожился.
        /// </summary>
        public struct OnEcsMonoBehaviorDestroyedSignal : IOneLabSignal
        {
            public IEcsMonoBehavior EcsMonoBehavior;
        }

        public struct CommandFilterTagSignal : IOneLabSignal
        {
            public TagFilter TagFilter;
            public int Entity;
        }

        public struct CommandInvokeGlobalTrigger : IOneLabSignal
        {
            public string Tag;
        }

        public struct OnLabEntityInitializedSignal : IOneLabSignal
        {
            public bool IsInitialized;
            public OneLabEntity OneLabEntity;
        }

        public struct CommandCameraFollowTransformSignal : IOneLabSignal
        {
            public bool FollowX;
            public bool FollowY;
            public int TargetEntity;
            public Vector2 Offset;
        }

        public struct CommandCreateVfxSignal : IOneLabSignal
        {
            public GameObject VfxPrefab;
            public Vector3 Position;
            public Vector3 Rotation;
            public Vector3 Scale;
            public bool IsLoop;
        }

        public struct CommandTryInvokeJumpSignal : IOneLabSignal
        {
        
        }
    }
}