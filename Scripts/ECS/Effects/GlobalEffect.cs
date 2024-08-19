
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/GlobalEffect")]
    public class GlobalEffect : EcsEffect
    {
        public void Invoke(string actionTag)
        {
            OneLab.Signal.RegistryRaise(new OneLabSignals.CommandInvokeGlobalTrigger { Tag = actionTag });
        }
    }
}