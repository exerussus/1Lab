using _1Lab.Scripts.ECS.Core;
using _1Lab.Scripts.ECS.Components;
using _1Lab.Scripts.Extensions;
using UnityEngine;

namespace _1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/Push")]
    public class PushEffect : EcsEffect
    {
        [SerializeField] private bool useTransformRotation = true;
        [SerializeField] private Vector2 direction = new Vector2(0, 1);
        [SerializeField] private float power;
        
        public void PushTarget(int originEntity, int targetEntity, Componenter componenter)
        {
            if (!Activated) return;
            if (!componenter.Has<PhysicalBodyData>(targetEntity)) return;
            
            ref var originTransform = ref componenter.Get<TransformData>(originEntity);
            ref var targetTransform = ref componenter.Get<TransformData>(targetEntity);

            var originPosition = (Vector2)originTransform.Value.position;
            var targetPosition = (Vector2)targetTransform.Value.position;

            var direction = (targetPosition - originPosition).normalized;
            ref var physicalBodyData = ref componenter.Get<PhysicalBodyData>(targetEntity);

            physicalBodyData.Rigidbody2D.velocity = direction * power;
        }
        
        public void PushOriginToDirection(int originEntity, Componenter componenter)
        {
            if (!Activated) return;
            if (!componenter.Has<PhysicalBodyData>(originEntity)) return;
            ref var physicalBodyData = ref componenter.Get<PhysicalBodyData>(originEntity);
            var resultDirection = useTransformRotation ? transform.rotation.ToDirection() : direction;
            physicalBodyData.Rigidbody2D.velocity = resultDirection * power;
        }
        
        public void PushOriginToDirection(int originEntity, int targetEntity, Componenter componenter)
        {
            if (!Activated) return;
            if (!componenter.Has<PhysicalBodyData>(originEntity)) return;
            ref var physicalBodyData = ref componenter.Get<PhysicalBodyData>(originEntity);
            var resultDirection = useTransformRotation ? transform.rotation.ToDirection() : direction;
            physicalBodyData.Rigidbody2D.velocity = resultDirection * power;
        }
        
        public void PushTargetToDirection(int originEntity, int targetEntity, Componenter componenter)
        {
            if (!Activated) return;
            if (!componenter.Has<PhysicalBodyData>(targetEntity)) return;
            ref var physicalBodyData = ref componenter.Get<PhysicalBodyData>(targetEntity);
            var resultDirection = useTransformRotation ? transform.rotation.ToDirection() : direction;
            physicalBodyData.Rigidbody2D.velocity = resultDirection * power;
        }
        
        public void PushOrigin(int originEntity, int targetEntity, Componenter componenter)
        {
            if (!Activated) return;
            if (!componenter.Has<PhysicalBodyData>(originEntity)) return;
            
            ref var originTransform = ref componenter.Get<TransformData>(originEntity);
            ref var targetTransform = ref componenter.Get<TransformData>(targetEntity);

            var originPosition = (Vector2)originTransform.Value.position;
            var targetPosition = (Vector2)targetTransform.Value.position;

            var direction = (originPosition - targetPosition).normalized;
            ref var physicalBodyData = ref componenter.Get<PhysicalBodyData>(originEntity);

            physicalBodyData.Rigidbody2D.velocity += direction * power;
        }
    }
}