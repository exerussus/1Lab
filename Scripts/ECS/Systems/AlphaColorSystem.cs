using Leopotam.EcsLite;
using OneLab.Scripts.ECS.Core;
using Source.Scripts.ECS.Components;

namespace Source.Scripts.ECS.Systems
{
    public class AlphaColorSystem : EasySystem
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

                if (alphaColorData.Visable)
                {
                    color.a -= alphaColorData.Speed;
                    visualData.SpriteRenderer.color = color;
                    if (visualData.SpriteRenderer.color.a <= 0)
                    {
                        alphaColorData.OnSuccess?.Invoke(entity, Componenter);
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
                        Componenter.Del<AlphaColorProcessData>(entity);
                    }
                }
            }
        }
    }
}