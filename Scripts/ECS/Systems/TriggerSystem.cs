using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using Exerussus._1Lab.Scripts.ECS.Triggers;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class TriggerSystem : EasySystem
    {
        private EcsFilter _timerTriggerFilter;
        private EcsFilter _keyPressedTriggerFilter;
        private OneLabPooler _pooler;

        protected override void Initialize()
        {
            _timerTriggerFilter = Componenter.Filter<TimerTriggerData>().End();
            _keyPressedTriggerFilter = Componenter.Filter<KeyPressedTriggerData>().End();
            GameShare.GetSharedObject(ref _pooler);
        }

        protected override void Update()
        {
            _timerTriggerFilter.Foreach(OnTimerTriggerUpdate);
            _keyPressedTriggerFilter.Foreach(OnKeyPressedTriggerUpdate);
        }

        private void OnKeyPressedTriggerUpdate(int entity)
        {
            ref var keyPressedTriggerData = ref _pooler.KeyPressedTrigger.Get(entity);
            if (Input.GetKeyDown(keyPressedTriggerData.Key)) keyPressedTriggerData.OnPressed?.Invoke(entity, Componenter);
        }

        private void OnTimerTriggerUpdate(int entity)
        {
            ref var timerTriggerData = ref _pooler.TimerTrigger.Get(entity);
            
            timerTriggerData.Timer += Time.deltaTime;
            if (timerTriggerData.Timer > timerTriggerData.Delay)
            {
                timerTriggerData.OnTick?.Invoke(entity, Componenter);
                if (timerTriggerData.IsLoop) timerTriggerData.Timer -= timerTriggerData.Delay;
                else _pooler.TimerTrigger.Del(entity);
            }
        }
    }
}