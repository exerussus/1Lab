
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Triggers
{
    [AddComponentMenu("1Lab/Triggers/EntityDeath")]
    public class EntityDeathTrigger : OneLabComponent
    {
        [SerializeField] private bool activatedOnStart = true;
        public UnityEvent<int, OneLabPooler> onDead;
        
        public override void Initialize()
        {
            if (activatedOnStart) ActivateTrigger();
        }

        public override void Destroy()
        {
            onDead?.Invoke(Entity, Pooler);
            DeactivateTrigger();
        }

        public void ActivateTrigger()
        {
            ref var entityDeathTriggerData = ref Pooler.EntityDeathTrigger.AddOrGet(Entity);
            entityDeathTriggerData.OnDead = onDead;
        }

        public void DeactivateTrigger()
        {
            Pooler.EntityDeathTrigger.Del(Entity);
        }
    }
}