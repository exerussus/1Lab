﻿using OneLab.Scripts.ECS.Core;
using OneLab.Scripts.ECS.Core.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/AlphaColor"), RequireComponent(typeof(VisualComponent))]
    public class AlphaColorComponent : EcsComponent
    {
        [SerializeField] private bool visable = true;
        [SerializeField] private float speed = 10;
        public UnityEvent<int, Componenter> onSuccess;
        private const float SpeedMultiply = 0.001f;
        
        public void Run()
        {
            ref var alphaColorProcessData = ref Componenter.AddOrGet<AlphaColorProcessData>(Entity);
            alphaColorProcessData.Visable = visable;
            alphaColorProcessData.Speed = speed * SpeedMultiply;
            alphaColorProcessData.OnSuccess = onSuccess;
        }
        
        public void Stop()
        {
            Componenter.Del<AlphaColorProcessData>(Entity);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                var color = spriteRenderer.color;
                color.a = visable ? 1 : 0;
                spriteRenderer.color = color;
            }
        }
    }

    public struct AlphaColorProcessData : IEcsComponent
    {
        public bool Visable;
        public float Speed;
        public UnityEvent<int, Componenter> OnSuccess;
    }
}