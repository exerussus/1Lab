using _1Lab.Scripts.ECS.Core;
using _1Lab.Scripts.ECS.Core.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace _1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/AlphaColor"), RequireComponent(typeof(VisualComponent))]
    public class AlphaColorComponent : EcsComponent
    {
        [SerializeField] public bool makeVisable = false;
        [SerializeField] public float speed = 10;
        public UnityEvent<int, Componenter> onSuccess;
        private const float SpeedMultiply = 0.001f;

        public void Run()
        {
            makeVisable = !makeVisable;
            ref var alphaColorProcessData = ref Componenter.AddOrGet<AlphaColorProcessData>(Entity);
            alphaColorProcessData.AlphaColor = this;
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
                color.a = makeVisable ? 1 : 0;
                spriteRenderer.color = color;
            }
        }
    }

    public struct AlphaColorProcessData : IEcsComponent
    {
        public AlphaColorComponent AlphaColor;
        public float Speed;
        public UnityEvent<int, Componenter> OnSuccess;
    }
}