using OneLab.Scripts.ECS.Core;
using Source.Scripts.ECS.Components;
using UnityEngine;

namespace Source.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/VisualColor")]
    public class VisualColorEffect : EcsEffect
    {
        [SerializeField] private Color color = Color.white;
        
        public void SetToOrigin(int originEntity, Componenter componenter)
        {
            SetColor(originEntity, componenter);
        }
        
        public void SetToOrigin(int originEntity, int targetEntity, Componenter componenter)
        {
            SetColor(originEntity, componenter);
        }
        
        public void SetToTarget(int originEntity, int targetEntity, Componenter componenter)
        {
            SetColor(targetEntity, componenter);
        }

        private void SetColor(int entity, Componenter componenter)
        {
            if (!componenter.Has<VisualData>(entity)) return;

            ref var visualData = ref componenter.Get<VisualData>(entity);
            visualData.SpriteRenderer.color = color;
        }
    }
}