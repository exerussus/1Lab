using _1Lab.Scripts.ECS.Core;
using _1Lab.Scripts.ECS.Systems;
using UnityEngine;

namespace _1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/GlobalEffect")]
    public class GlobalEffect : EcsEffect
    {
        public void Invoke(string actionTag)
        {
            OneLab.Signal.RegistryRaise(new CommandInvokeGlobalTrigger { Tag = actionTag });
        }
    }
}