
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
            ref var globalTriggerData = ref Componenter.AddOrGet<OneLabData.GlobalTriggerData>(Entity);
            globalTriggerData.Value = this;
            globalTriggerData.Tags = tags;
        }

        public void Stop()
        {
            Componenter.Del<OneLabData.GlobalTriggerData>(Entity);
        }
    }
}