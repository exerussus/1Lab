
using Leopotam.EcsLite;
using Exerussus._1Lab.Scripts.ECS.Core;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class AlphaColorSystem : OneLabSystem
    {
        private EcsFilter _alphaColorFilter;

        protected override void Initialize()
        {
            _alphaColorFilter = Componenter.Filter<OneLabData.AlphaColorProcessData>().Inc<OneLabData.VisualData>().End();
        }

        protected override void Update()
        {
            foreach (var entity in _alphaColorFilter)
            {
                ref var alphaColorData = ref Pooler.AlphaColorProcess.Get(entity);
                ref var visualData = ref Pooler.Visual.Get(entity);
                var color = visualData.SpriteRenderer.color;

                if (!alphaColorData.AlphaColor.makeVisable)
                {
                    color.a -= alphaColorData.Speed;
                    visualData.SpriteRenderer.color = color;
                    if (visualData.SpriteRenderer.color.a <= 0)
                    {
                        alphaColorData.OnSuccess?.Invoke(entity, Componenter, Pooler);
                        alphaColorData.AlphaColor.makeVisable = false;
                        Pooler.AlphaColorProcess.Del(entity);
                    }
                }
                else
                {
                    color.a += alphaColorData.Speed;
                    visualData.SpriteRenderer.color = color;
                    if (visualData.SpriteRenderer.color.a >= 1)
                    {
                        alphaColorData.OnSuccess?.Invoke(entity, Componenter, Pooler);
                        alphaColorData.AlphaColor.makeVisable = true;
                        Pooler.AlphaColorProcess.Del(entity);
                    }
                }
            }
        }
    }
}