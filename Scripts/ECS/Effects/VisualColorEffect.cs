
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
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
            if (!componenter.Has<OneLabData.VisualData>(entity)) return;

            ref var visualData = ref componenter.Get<OneLabData.VisualData>(entity);
            visualData.SpriteRenderer.color = color;
        }
    }
}