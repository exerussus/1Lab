
using _1Lab.Scripts.ECS.Core;
using _1Lab.Scripts.SignalSystem;
using UnityEngine;

namespace _1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/Destroy")]
    public class DestroyEffect : EcsEffect
    {
        [SerializeField] private bool immediately = true;
        
        public void DestroyTarget(int originEntity, int targetEntity, Componenter componenter)
        {
            if (!Activated) return;
            DestroyEntity(targetEntity);
        }
        
        public void DestroyOrigin(int originEntity, int targetEntity, Componenter componenter)
        {
            if (!Activated) return;
            DestroyEntity(originEntity);
        }
        
        public void DestroyOrigin(int originEntity, Componenter componenter)
        {
            if (!Activated) return;
            DestroyEntity(originEntity);
        }

        private void DestroyEntity(int entity)
        {
            Signal.RegistryRaise(new CommandKillEntitySignal {Entity = entity, Immediately = immediately});
        }
    }
}