using Exerussus._1Lab.Scripts.ECS.Core;
using Exerussus._1Lab.Scripts.ECS.Systems;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
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