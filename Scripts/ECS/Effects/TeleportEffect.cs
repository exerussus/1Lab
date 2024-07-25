using _1Lab.Scripts.ECS.Core;
using _1Lab.Scripts.ECS.Components;
using UnityEngine;

namespace _1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/Teleport")]
    public class TeleportEffect : EcsEffect
    {
        [SerializeField] private Vector2 position;
        private readonly Color _pointColor = Color.cyan;

        public void TeleportOrigin(int originEntity, Componenter componenter)
        {
            Teleport(originEntity, componenter);
        }
        
        public void TeleportOrigin(int originEntity, int targetEntity, Componenter componenter)
        {
            Teleport(originEntity, componenter);
        }
        
        public void TeleportTarget(int originEntity, int targetEntity, Componenter componenter)
        {
            Teleport(targetEntity, componenter);
        }

        private void Teleport(int entity, Componenter componenter)
        {
            ref var transformData = ref componenter.Get<TransformData>(entity);
            var result = transformData.Value.position;
            result.x = position.x;
            result.y = position.y;
            transformData.Value.position = result;
            
            if (componenter.Has<PhysicalBodyData>(entity))
            {
                ref var physicalBodyData = ref componenter.Get<PhysicalBodyData>(entity);
                physicalBodyData.Rigidbody2D.velocity = Vector2.zero;
            }
            
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = _pointColor;
            Gizmos.DrawSphere(position, 0.1f);
        }
    }
}