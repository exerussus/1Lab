
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/VisualColor")]
    public class VisualColorEffect : EcsEffect
    {
        [SerializeField] private Color color = Color.white;
        
        public void SetToOrigin(int originEntity, OneLabPooler pooler)
        {
            SetColor(originEntity, pooler);
        }
        
        public void SetToOrigin(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            SetColor(originEntity, pooler);
        }
        
        public void SetToTarget(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            SetColor(targetEntity, pooler);
        }

        private void SetColor(int entity, OneLabPooler pooler)
        {
            if (!pooler.Visual.Has(entity)) return;

            ref var visualData = ref pooler.Visual.Get(entity);
            visualData.SpriteRenderer.color = color;
        }
    }
}