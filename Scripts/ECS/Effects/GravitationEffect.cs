
using Exerussus._1EasyEcs.Scripts.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/Gravitation")]
    public class GravitationEffect : EcsEffect
    {
        [SerializeField] private float gravity;

        public void SetToOrigin(int originEntity, Componenter componenter)
        {
            Set(originEntity, componenter);
        }
        
        public void SetToOrigin(int originEntity, int targetEntity, Componenter componenter)
        {
            Set(originEntity, componenter);
        }
        
        public void SetToTarget(int originEntity, int targetEntity, Componenter componenter)
        {
            Set(targetEntity, componenter);
        }

        private void Set(int entity, Componenter componenter)
        {
            if (!componenter.Has<RigidBody2DData>(entity)) return;
            
            ref var physicalBodyData = ref componenter.Get<RigidBody2DData>(entity);
            physicalBodyData.Value.gravityScale = gravity;
        }
    }
}