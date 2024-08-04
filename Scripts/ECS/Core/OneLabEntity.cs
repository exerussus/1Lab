
using Exerussus._1EasyEcs.Scripts.Core;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public class OneLabEntity : EcsMonoBehavior
    {
        public string[] tags;
        
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