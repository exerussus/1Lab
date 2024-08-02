
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/Visual"), RequireComponent(typeof(SpriteRenderer))]
    public class VisualComponent : OneLabComponent
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