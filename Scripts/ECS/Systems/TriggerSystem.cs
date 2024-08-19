
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class TriggerSystem : OneLabSystem
    {
        private EcsFilter _timerTriggerFilter;
        private EcsFilter _keyPressedTriggerFilter;

        protected override void Initialize()
        {
            _timerTriggerFilter = Componenter.Filter<OneLabData.TimerTriggerData>().End();
            _keyPressedTriggerFilter = Componenter.Filter<OneLabData.KeyPressedTriggerData>().End();
        }

        protected override void Update()
        {
            _timerTriggerFilter.Foreach(OnTimerTriggerUpdate);
            _keyPressedTriggerFilter.Foreach(OnKeyPressedTriggerUpdate);
        }

        private void OnKeyPressedTriggerUpdate(int entity)
        {
            ref var keyPressedTriggerData = ref Pooler.KeyPressedTrigger.Get(entity);
            if (Input.GetKeyDown(keyPressedTriggerData.Key)) keyPressedTriggerData.OnPressed?.Invoke(entity, Componenter, Pooler);
        }

        private void OnTimerTriggerUpdate(int entity)
        {
            ref var timerTriggerData = ref Pooler.TimerTrigger.Get(entity);
            
            timerTriggerData.Timer += Time.deltaTime;
            if (timerTriggerData.Timer > timerTriggerData.Delay)
            {
                timerTriggerData.OnTick?.Invoke(entity, Componenter, Pooler);
                if (timerTriggerData.IsLoop) timerTriggerData.Timer -= timerTriggerData.Delay;
                else Pooler.TimerTrigger.Del(entity);
            }
        }
    }
}