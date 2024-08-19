﻿using Exerussus._1EasyEcs.Scripts.Core;
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
        
        public void PushTarget(int originEntity, int targetEntity, Componenter componenter)
        {
            if (!Activated) return;
            if (!componenter.Has<OneLabData.RigidBody2DData>(targetEntity)) return;
            
            ref var originTransform = ref componenter.Get<OneLabData.TransformData>(originEntity);
            ref var targetTransform = ref componenter.Get<OneLabData.TransformData>(targetEntity);

            var originPosition = (Vector2)originTransform.Value.position;
            var targetPosition = (Vector2)targetTransform.Value.position;

            var direction = (targetPosition - originPosition).normalized;
            ref var physicalBodyData = ref componenter.Get<OneLabData.RigidBody2DData>(targetEntity);

            physicalBodyData.Value.velocity = direction * power;
        }
        
        public void PushOriginToDirection(int originEntity, Componenter componenter)
        {
            if (!Activated) return;
            if (!componenter.Has<OneLabData.RigidBody2DData>(originEntity)) return;
            ref var physicalBodyData = ref componenter.Get<OneLabData.RigidBody2DData>(originEntity);
            var resultDirection = useTransformRotation ? transform.rotation.ToDirection() : direction;
            physicalBodyData.Value.velocity = resultDirection * power;
        }
        
        public void PushOriginToDirection(int originEntity, int targetEntity, Componenter componenter)
        {
            if (!Activated) return;
            if (!componenter.Has<OneLabData.RigidBody2DData>(originEntity)) return;
            ref var physicalBodyData = ref componenter.Get<OneLabData.RigidBody2DData>(originEntity);
            var resultDirection = useTransformRotation ? transform.rotation.ToDirection() : direction;
            physicalBodyData.Value.velocity = resultDirection * power;
        }
        
        public void PushTargetToDirection(int originEntity, int targetEntity, Componenter componenter)
        {
            if (!Activated) return;
            if (!componenter.Has<OneLabData.RigidBody2DData>(targetEntity)) return;
            ref var physicalBodyData = ref componenter.Get<OneLabData.RigidBody2DData>(targetEntity);
            var resultDirection = useTransformRotation ? transform.rotation.ToDirection() : direction;
            physicalBodyData.Value.velocity = resultDirection * power;
        }
        
        public void PushOrigin(int originEntity, int targetEntity, Componenter componenter)
        {
            if (!Activated) return;
            if (!componenter.Has<OneLabData.RigidBody2DData>(originEntity)) return;
            
            ref var originTransform = ref componenter.Get<OneLabData.TransformData>(originEntity);
            ref var targetTransform = ref componenter.Get<OneLabData.TransformData>(targetEntity);

            var originPosition = (Vector2)originTransform.Value.position;
            var targetPosition = (Vector2)targetTransform.Value.position;

            var direction = (originPosition - targetPosition).normalized;
            ref var physicalBodyData = ref componenter.Get<OneLabData.RigidBody2DData>(originEntity);

            physicalBodyData.Value.velocity += direction * power;
        }
    }
}