using Leopotam.EcsLite;
using _1Lab.Scripts.ECS.Core;
using _1Lab.Scripts.ECS.Triggers;
using UnityEngine;

namespace _1Lab.Scripts.ECS.Systems
{
    public class TriggerSystem : EasySystem
    {
        private EcsFilter _timerTriggerFilter;
        private EcsFilter _keyPressedTriggerFilter;
        
        protected override void Initialize()
        {
            _timerTriggerFilter = Componenter.Filter<TimerTriggerData>().End();
            _keyPressedTriggerFilter = Componenter.Filter<KeyPressedTriggerData>().End();
        }

        protected override void Update()
        {
            _timerTriggerFilter.Foreach(OnTimerTriggerUpdate);
            _keyPressedTriggerFilter.Foreach(OnKeyPressedTriggerUpdate);
        }

        private void OnKeyPressedTriggerUpdate(int entity)
        {
            ref var keyPressedTriggerData = ref Componenter.Get<KeyPressedTriggerData>(entity);
            if (Input.GetKeyDown(keyPressedTriggerData.Key)) keyPressedTriggerData.OnPressed?.Invoke(entity, Componenter);
        }

        private void OnTimerTriggerUpdate(int entity)
        {
            ref var timerTriggerData = ref Componenter.Get<TimerTriggerData>(entity);
            
            timerTriggerData.Timer += Time.deltaTime;
            if (timerTriggerData.Timer > timerTriggerData.Delay)
            {
                timerTriggerData.OnTick?.Invoke(entity, Componenter);
                if (timerTriggerData.IsLoop) timerTriggerData.Timer -= timerTriggerData.Delay;
                else Componenter.Del<TimerTriggerData>(entity);
            }
        }
    }
}