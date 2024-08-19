
using Exerussus._1Extensions.Scripts.Extensions;
using Exerussus._1Lab.Scripts.Core;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public class OneLabEntity : EcsMonoBehavior
    {
        public string[] tags;
        
        public void Start()
        {
            Initialize(OneLab.Componenter, OneLab.Signal);
            if (tags.IsNotEmpty())
            {
                ref var oneLabEntityData = ref Componenter.AddOrGet<OneLabData.TagsData>(Entity);
                oneLabEntityData.Values = tags;
                Signal.RegistryRaise(new OneLabSignals.OnLabEntityInitializedSignal
                {
                    IsInitialized = true,
                    OneLabEntity = this
                });
            }
        }
    }
}