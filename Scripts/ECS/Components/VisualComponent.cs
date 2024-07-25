using OneLab.Scripts.ECS.Core.Interfaces;
using OneLab.Scripts.ECS.Core;
using UnityEngine;

namespace Source.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/Visual"), RequireComponent(typeof(SpriteRenderer))]
    public class VisualComponent : EcsComponent
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public override void Initialize()
        {
            ref var visualData = ref Componenter.AddOrGet<VisualData>(Entity);
            visualData.SpriteRenderer = spriteRenderer;
        }

        public override void Destroy()
        {
            Componenter.Del<VisualData>(Entity);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public struct VisualData : IEcsComponent
    {
        public SpriteRenderer SpriteRenderer;
    }
}