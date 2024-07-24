using OneLab.Scripts.ECS.Core.Interfaces;
using OneLab.Scripts.ECS.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.ECS.Triggers
{
    [AddComponentMenu("1Lab/Triggers/Timer")]
    public class TimerTrigger : EcsComponent
    {
        [SerializeField] private bool autoStart;
        [SerializeField] private float delay = 1f;
        [SerializeField] private bool isLoop;

        public UnityEvent<int, Componenter> onTick;

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

    public struct TimerTriggerData : IEcsComponent
    {
        public UnityEvent<int, Componenter> OnTick;
        public float Delay;
        public float Timer;
        public bool IsLoop;
    }
}