
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/VisualColor")]
    public class VisualColorEffect : EcsEffect
    {
        [SerializeField] private Color color = Color.white;
        
        public void SetToOrigin(int originEntity, Componenter componenter, OneLabPooler pooler)
        {
            SetColor(originEntity, componenter, pooler);
        }
        
        public void SetToOrigin(int originEntity, int targetEntity, Componenter componenter, OneLabPooler pooler)
        {
            SetColor(originEntity, componenter, pooler);
        }
        
        public void SetToTarget(int originEntity, int targetEntity, Componenter componenter, OneLabPooler pooler)
        {
            SetColor(targetEntity, componenter, pooler);
        }

        private void SetColor(int entity, Componenter componenter, OneLabPooler pooler)
        {
            if (!pooler.Visual.Has(entity)) return;

            ref var visualData = ref pooler.Visual.Get(entity);
            visualData.SpriteRenderer.color = color;
        }
    }
}