using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/Teleport")]
    public class TeleportEffect : EcsEffect
    {
        [SerializeField] private Vector2 position;
        private readonly Color _pointColor = Color.cyan;

        public void TeleportOrigin(int originEntity, Componenter<IOneLabEcsData> componenter)
        {
            Teleport(originEntity, componenter);
        }
        
        public void TeleportOrigin(int originEntity, int targetEntity, Componenter<IOneLabEcsData> componenter)
        {
            Teleport(originEntity, componenter);
        }
        
        public void TeleportTarget(int originEntity, int targetEntity, Componenter<IOneLabEcsData> componenter)
        {
            Teleport(targetEntity, componenter);
        }

        private void Teleport(int entity, Componenter<IOneLabEcsData> componenter)
        {
            ref var transformData = ref componenter.Get<TransformData>(entity);
            var result = transformData.Value.position;
            result.x = position.x;
            result.y = position.y;
            transformData.Value.position = result;
            
            if (componenter.Has<RigidBody2DData>(entity))
            {
                ref var physicalBodyData = ref componenter.Get<RigidBody2DData>(entity);
                physicalBodyData.Value.velocity = Vector2.zero;
            }
            
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = _pointColor;
            Gizmos.DrawSphere(position, 0.1f);
        }
    }
}