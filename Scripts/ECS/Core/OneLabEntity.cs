
using System;
using Exerussus._1EasyEcs.Scripts.Core;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public class OneLabEntity : EcsMonoBehavior
    {
        public Tags tags;
        
        public void Start()
        {
            Initialize(OneLab.Componenter, OneLab.Signal);
            ref var oneLabEntityData = ref Componenter.AddOrGet<OneLabEntityData>(Entity);
            oneLabEntityData.Value = this;
            Signal.RegistryRaise(new OnLabEntityInitializedSignal
            {
                IsInitialized = true,
                OneLabEntity = this
            });
        }
        
        [Serializable]
        public class Tags
        {
            public string[] values;
        }
    }

    public struct OneLabEntityData : IEcsComponent
    {
        public OneLabEntity Value;
    }

    public struct OnLabEntityInitializedSignal
    {
        public bool IsInitialized;
        public OneLabEntity OneLabEntity;
    }
}