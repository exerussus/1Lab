
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/Gravitation")]
    public class GravitationEffect : EcsEffect
    {
        [SerializeField] private float gravity;

        public void SetToOrigin(int originEntity, OneLabPooler pooler)
        {
            Set(originEntity, pooler);
        }
        
        public void SetToOrigin(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            Set(originEntity, pooler);
        }
        
        public void SetToTarget(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            Set(targetEntity, pooler);
        }

        private void Set(int entity, OneLabPooler pooler)
        {
            if (!pooler.RigidBody2D.Has(entity)) return;
            
            ref var physicalBodyData = ref pooler.RigidBody2D.Get(entity);
            physicalBodyData.Value.gravityScale = gravity;
        }
    }
}