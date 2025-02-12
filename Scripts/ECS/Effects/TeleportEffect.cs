﻿
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/Teleport")]
    public class TeleportEffect : EcsEffect
    {
        [SerializeField] private Vector2 position;
        private readonly Color _pointColor = Color.cyan;

        public void TeleportOrigin(int originEntity, OneLabPooler pooler)
        {
            Teleport(originEntity, pooler);
        }
        
        public void TeleportOrigin(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            Teleport(originEntity, pooler);
        }
        
        public void TeleportTarget(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            Teleport(targetEntity, pooler);
        }

        private void Teleport(int entity, OneLabPooler pooler)
        {
            ref var transformData = ref pooler.Transform.Get(entity);
            var result = transformData.Value.position;
            result.x = position.x;
            result.y = position.y;
            transformData.Value.position = result;
            
            if (pooler.RigidBody2D.Has(entity))
            {
                ref var physicalBodyData = ref pooler.RigidBody2D.Get(entity);
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