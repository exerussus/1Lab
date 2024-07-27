using _1Lab.Scripts.ECS.Core;
using _1Lab.Scripts.ECS.Core.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Plugins._1Lab.Scripts.ECS.Triggers
{
    [AddComponentMenu("1Lab/Triggers/GlobalTrigger")]
    public class GlobalTrigger : EcsComponent
    {
        public bool autoRun = true;
        public string[] tags;
        public UnityEvent onAction;
        
        public override void Initialize()
        {
            if (autoRun) Run();
        }

        public void Run()
        {
            ref var globalTriggerData = ref Componenter.AddOrGet<GlobalTriggerData>(Entity);
            globalTriggerData.Value = this;
        }

        public void Stop()
        {
            Componenter.Del<GlobalTriggerData>(Entity);
        }
    }

    public struct GlobalTriggerData : IEcsComponent
    {
        public GlobalTrigger Value;
    }
}