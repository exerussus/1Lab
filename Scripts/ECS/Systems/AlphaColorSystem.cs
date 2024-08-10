using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Exerussus._1Lab.Scripts.ECS.Components;
using Exerussus._1Lab.Scripts.ECS.Core;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class AlphaColorSystem : EasySystem<IOneLabEcsData>
    {
        private EcsFilter _alphaColorFilter;

        protected override void Initialize()
        {
            _alphaColorFilter = Componenter.Filter<AlphaColorProcessData>().Inc<VisualData>().End();
        }

        protected override void Update()
        {
            foreach (var entity in _alphaColorFilter)
            {
                ref var alphaColorData = ref Componenter.Get<AlphaColorProcessData>(entity);
                ref var visualData = ref Componenter.Get<VisualData>(entity);
                var color = visualData.SpriteRenderer.color;

                if (!alphaColorData.AlphaColor.makeVisable)
                {
                    color.a -= alphaColorData.Speed;
                    visualData.SpriteRenderer.color = color;
                    if (visualData.SpriteRenderer.color.a <= 0)
                    {
                        alphaColorData.OnSuccess?.Invoke(entity, Componenter);
                        alphaColorData.AlphaColor.makeVisable = false;
                        Componenter.Del<AlphaColorProcessData>(entity);
                    }
                }
                else
                {
                    color.a += alphaColorData.Speed;
                    visualData.SpriteRenderer.color = color;
                    if (visualData.SpriteRenderer.color.a >= 1)
                    {
                        alphaColorData.OnSuccess?.Invoke(entity, Componenter);
                        alphaColorData.AlphaColor.makeVisable = true;
                        Componenter.Del<AlphaColorProcessData>(entity);
                    }
                }
            }
        }
    }
}