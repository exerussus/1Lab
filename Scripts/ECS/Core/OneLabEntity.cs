
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.Scripts.Extensions;
using Exerussus._1Lab.Scripts.ECS.Components;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public class OneLabEntity : EcsMonoBehavior<IOneLabEcsData>
    {
        public string[] tags;
        
        public void Start()
        {
            Initialize(OneLab.Componenter, OneLab.Signal);
            if (tags.IsNotEmpty())
            {
                ref var oneLabEntityData = ref Componenter.AddOrGet<TagsData>(Entity);
                oneLabEntityData.Values = tags;
                Signal.RegistryRaise(new OnLabEntityInitializedSignal
                {
                    IsInitialized = true,
                    OneLabEntity = this
                });
            }
        }
    }

    public struct TagsData : IOneLabEcsData
    {
        public string[] Values;
    }

    public struct OnLabEntityInitializedSignal
    {
        public bool IsInitialized;
        public OneLabEntity OneLabEntity;
    }
}