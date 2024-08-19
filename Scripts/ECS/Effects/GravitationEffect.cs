
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/Gravitation")]
    public class GravitationEffect : EcsEffect
    {
        [SerializeField] private float gravity;

        public void SetToOrigin(int originEntity, Componenter componenter, OneLabPooler pooler)
        {
            Set(originEntity, componenter, pooler);
        }
        
        public void SetToOrigin(int originEntity, int targetEntity, Componenter componenter, OneLabPooler pooler)
        {
            Set(originEntity, componenter, pooler);
        }
        
        public void SetToTarget(int originEntity, int targetEntity, Componenter componenter, OneLabPooler pooler)
        {
            Set(targetEntity, componenter, pooler);
        }

        private void Set(int entity, Componenter componenter, OneLabPooler pooler)
        {
            if (!componenter.Has<OneLabData.RigidBody2DData>(entity)) return;
            
            ref var physicalBodyData = ref componenter.Get<OneLabData.RigidBody2DData>(entity);
            physicalBodyData.Value.gravityScale = gravity;
        }
    }
}