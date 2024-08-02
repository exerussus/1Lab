﻿
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Components;
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
            if (!componenter.Has<PhysicalBodyData>(entity)) return;
            
            ref var physicalBodyData = ref componenter.Get<PhysicalBodyData>(entity);
            physicalBodyData.Rigidbody2D.gravityScale = gravity;
        }
    }
}