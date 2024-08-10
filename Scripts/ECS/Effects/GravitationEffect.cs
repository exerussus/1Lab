
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/Gravitation")]
    public class GravitationEffect : EcsEffect
    {
        [SerializeField] private float gravity;

        public void SetToOrigin(int originEntity, Componenter<IOneLabEcsData> componenter)
        {
            Set(originEntity, componenter);
        }
        
        public void SetToOrigin(int originEntity, int targetEntity, Componenter<IOneLabEcsData> componenter)
        {
            Set(originEntity, componenter);
        }
        
        public void SetToTarget(int originEntity, int targetEntity, Componenter<IOneLabEcsData> componenter)
        {
            Set(targetEntity, componenter);
        }

        private void Set(int entity, Componenter<IOneLabEcsData> componenter)
        {
            if (!componenter.Has<RigidBody2DData>(entity)) return;
            
            ref var physicalBodyData = ref componenter.Get<RigidBody2DData>(entity);
            physicalBodyData.Value.gravityScale = gravity;
        }
    }
}