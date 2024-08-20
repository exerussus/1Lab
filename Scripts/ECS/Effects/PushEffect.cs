using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.Scripts.Extensions;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/Push")]
    public class PushEffect : EcsEffect
    {
        [SerializeField] private bool useTransformRotation = true;
        [SerializeField] private Vector2 direction = new Vector2(0, 1);
        [SerializeField] private float power;
        
        public void PushTarget(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            if (!Activated) return;
            if (!pooler.RigidBody2D.Has(targetEntity)) return;
            
            ref var originTransform = ref pooler.Transform.Get(originEntity);
            ref var targetTransform = ref pooler.Transform.Get(targetEntity);

            var originPosition = (Vector2)originTransform.Value.position;
            var targetPosition = (Vector2)targetTransform.Value.position;

            var direction = (targetPosition - originPosition).normalized;
            ref var physicalBodyData = ref pooler.RigidBody2D.Get(targetEntity);

            physicalBodyData.Value.velocity = direction * power;
        }
        
        public void PushOriginToDirection(int originEntity, OneLabPooler pooler)
        {
            if (!Activated) return;
            if (!pooler.RigidBody2D.Has(originEntity)) return;
            ref var physicalBodyData = ref pooler.RigidBody2D.Get(originEntity);
            var resultDirection = useTransformRotation ? transform.rotation.ToDirection() : direction;
            physicalBodyData.Value.velocity = resultDirection * power;
        }
        
        public void PushOriginToDirection(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            if (!Activated) return;
            if (!pooler.RigidBody2D.Has(originEntity)) return;
            ref var physicalBodyData = ref pooler.RigidBody2D.Get(originEntity);
            var resultDirection = useTransformRotation ? transform.rotation.ToDirection() : direction;
            physicalBodyData.Value.velocity = resultDirection * power;
        }
        
        public void PushTargetToDirection(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            if (!Activated) return;
            if (!pooler.RigidBody2D.Has(targetEntity)) return;
            ref var physicalBodyData = ref pooler.RigidBody2D.Get(targetEntity);
            var resultDirection = useTransformRotation ? transform.rotation.ToDirection() : direction;
            physicalBodyData.Value.velocity = resultDirection * power;
        }
        
        public void PushOrigin(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            if (!Activated) return;
            if (!pooler.RigidBody2D.Has(originEntity)) return;
            
            ref var originTransform = ref pooler.Transform.Get(originEntity);
            ref var targetTransform = ref pooler.Transform.Get(targetEntity);

            var originPosition = (Vector2)originTransform.Value.position;
            var targetPosition = (Vector2)targetTransform.Value.position;

            var direction = (originPosition - targetPosition).normalized;
            ref var physicalBodyData = ref pooler.RigidBody2D.Get(originEntity);

            physicalBodyData.Value.velocity += direction * power;
        }
    }
}