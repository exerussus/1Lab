using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Triggers
{
    [AddComponentMenu("1Lab/Triggers/GlobalTrigger")]
    public class GlobalTrigger : OneLabComponent
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