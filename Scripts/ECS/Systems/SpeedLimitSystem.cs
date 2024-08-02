using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Exerussus._1Lab.Scripts.ECS.Components;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class SpeedLimitSystem : EasySystem
    {
        private EcsFilter _speedXLimitFilter;
        private EcsFilter _speedYLimitFilter;
        
        protected override void Initialize()
        {
            _speedXLimitFilter = Componenter.Filter<SpeedLimitXData>().End();
            _speedYLimitFilter = Componenter.Filter<SpeedLimitYData>().End();
        }

        protected override void Update()
        {
            _speedXLimitFilter.Foreach(OnLimitXUpdate);
            _speedYLimitFilter.Foreach(OnLimitYUpdate);
        }

        private void OnLimitXUpdate(int entity)
        {
            ref var speedLimitXData = ref Componenter.Get<SpeedLimitXData>(entity);
            if (Mathf.Abs(speedLimitXData.PhysicalBody.Rigidbody2D.velocity.x) > speedLimitXData.Limit)
            {
                var directionMultiply = speedLimitXData.PhysicalBody.Rigidbody2D.velocity.x >= 0 ? 1 : -1;
                var velocity = speedLimitXData.PhysicalBody.Rigidbody2D.velocity;
                velocity.x = speedLimitXData.Limit * directionMultiply;
                speedLimitXData.PhysicalBody.Rigidbody2D.velocity = velocity;
            }
        }

        private void OnLimitYUpdate(int entity)
        {
            ref var speedLimitYData = ref Componenter.Get<SpeedLimitYData>(entity);
            if (Mathf.Abs(speedLimitYData.PhysicalBody.Rigidbody2D.velocity.y) > speedLimitYData.Limit)
            {
                var directionMultiply = speedLimitYData.PhysicalBody.Rigidbody2D.velocity.y >= 0 ? 1 : -1;
                var velocity = speedLimitYData.PhysicalBody.Rigidbody2D.velocity;
                velocity.y = speedLimitYData.Limit * directionMultiply;
                speedLimitYData.PhysicalBody.Rigidbody2D.velocity = velocity;
            }
        }
    }
}