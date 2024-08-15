using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Triggers
{
    [AddComponentMenu("1Lab/Triggers/EntityDeath")]
    public class EntityDeathTrigger : OneLabComponent
    {
        [SerializeField] private bool activatedOnStart = true;
        public UnityEvent<int, Componenter> onDead;
        
        public override void Initialize()
        {
            if (activatedOnStart) ActivateTrigger();
        }

        public override void Destroy()
        {
            onDead?.Invoke(Entity, Componenter);
            DeactivateTrigger();
        }

        public void ActivateTrigger()
        {
            ref var entityDeathTriggerData = ref Componenter.AddOrGet<EntityDeathTriggerData>(Entity);
            entityDeathTriggerData.OnDead = onDead;
        }

        public void DeactivateTrigger()
        {
            Componenter.Del<EntityDeathTriggerData>(Entity);
        }
    }

    public struct EntityDeathTriggerData : IOneLabEcsData
    {
        public UnityEvent<int, Componenter> OnDead;
    }
}