using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Triggers
{
    [AddComponentMenu("1Lab/Triggers/Timer")]
    public class TimerTrigger : OneLabComponent
    {
        [SerializeField] private bool autoStart = true;
        [SerializeField] private float delay = 1f;
        [SerializeField] private bool isLoop;

        public UnityEvent<int, Componenter<IOneLabEcsData>> onTick;

        public override void Initialize()
        {
            if (autoStart) Run();
        }

        public override void Destroy()
        {
            Stop();
        }
        
        public void Run()
        {
            ref var timerTriggerData = ref Componenter.AddOrGet<TimerTriggerData>(Entity);
            timerTriggerData.OnTick = onTick;
            timerTriggerData.Delay = delay;
            timerTriggerData.IsLoop = isLoop;
        }

        public void Stop()
        {
            Componenter.Del<TimerTriggerData>(Entity);
        }
    }

    public struct TimerTriggerData : IOneLabEcsData
    {
        public UnityEvent<int, Componenter<IOneLabEcsData>> OnTick;
        public float Delay;
        public float Timer;
        public bool IsLoop;
    }
}