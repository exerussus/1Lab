﻿
using _1Lab.Scripts.ECS.Core;

namespace _1Lab.Scripts.SignalSystem
{
    /// <summary>
    /// MonoBehaviourView проинициализировался.
    /// </summary>
    public struct OnEcsMonoBehaviorInitializedSignal
    {
        public EcsMonoBehavior EcsMonoBehavior;
    }
    
    /// <summary>
    /// MonoBehaviourView уничтожился.
    /// </summary>
    public struct OnEcsMonoBehaviorStartDestroySignal
    {
        public EcsMonoBehavior EcsMonoBehavior;
    }
    
    /// <summary>
    /// MonoBehaviourView уничтожился.
    /// </summary>
    public struct OnEcsMonoBehaviorDestroyedSignal
    {
        public EcsMonoBehavior EcsMonoBehavior;
    }
    
    /// <summary>
    /// Entity потеряло всё здоровье
    /// </summary>
    public struct CommandKillEntitySignal
    {
        public int Entity;
        public bool Immediately;
    }

}