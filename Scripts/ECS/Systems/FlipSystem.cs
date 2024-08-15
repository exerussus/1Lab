using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Components;
using Exerussus._1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class FlipSystem : EasySystem<IOneLabEcsData>
    {
        private EcsFilter _fliperFilter;
        private Camera _camera;
        private Pooler _pooler;

        protected override void Initialize()
        {
            _camera = Camera.main;
            _fliperFilter = Componenter.Filter<FliperData>().End();
            _pooler = GameShare.GetSharedObject<Pooler>();
        }

        protected override void Update()
        {
            _fliperFilter.Foreach(OnFliperUpdate);
        }

        private void OnFliperUpdate(int entity)
        {
            ref var fliperData = ref _pooler.Fliper.Get(entity);

            if (fliperData.Value.Mode == FliperComponent.ControlType.Mouse)
            {
                ref var transformData = ref _pooler.Transform.Get(entity);
                var mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
                if (transformData.Value.position.x > mouseWorldPosition.x)
                {
                    if (!fliperData.Value.Fliped) fliperData.Value.Flip(true);
                }
                else if (fliperData.Value.Fliped) fliperData.Value.Flip(false);
            }
            else if (fliperData.Value.Mode == FliperComponent.ControlType.Axis)
            {
                var input = Input.GetAxis("Horizontal");
                if (input < -0.1 && !fliperData.Value.Fliped) fliperData.Value.Flip(true);
                else if (input > 0.1 && fliperData.Value.Fliped) fliperData.Value.Flip(false);
            }
        }
    }
}