
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/AlphaColor"), RequireComponent(typeof(VisualComponent))]
    public class AlphaColorComponent : OneLabComponent
    {
        [SerializeField] public bool makeVisable = false;
        [SerializeField] public float speed = 10;
        public UnityEvent<int, Componenter, OneLabPooler> onSuccess;
        private const float SpeedMultiply = 0.001f;

        public void Run()
        {
            makeVisable = !makeVisable;
            ref var alphaColorProcessData = ref Pooler.AlphaColorProcess.AddOrGet(Entity);
            alphaColorProcessData.AlphaColor = this;
            alphaColorProcessData.Speed = speed * SpeedMultiply;
            alphaColorProcessData.OnSuccess = onSuccess;
        }
        
        public void Stop()
        {
            Pooler.AlphaColorProcess.Del(Entity);
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
}