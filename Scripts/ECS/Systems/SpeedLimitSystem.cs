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
            ref var rbData = ref Componenter.Get<RigidBody2DData>(entity);
            if (Mathf.Abs(rbData.Value.velocity.x) > speedLimitXData.Limit)
            {
                var directionMultiply = rbData.Value.velocity.x >= 0 ? 1 : -1;
                var velocity = rbData.Value.velocity;
                velocity.x = speedLimitXData.Limit * directionMultiply;
                rbData.Value.velocity = velocity;
            }
        }

        private void OnLimitYUpdate(int entity)
        {
            ref var speedLimitYData = ref Componenter.Get<SpeedLimitYData>(entity);
            ref var rbData = ref Componenter.Get<RigidBody2DData>(entity);
            if (Mathf.Abs(rbData.Value.velocity.y) > speedLimitYData.Limit)
            {
                var directionMultiply = rbData.Value.velocity.y >= 0 ? 1 : -1;
                var velocity = rbData.Value.velocity;
                velocity.y = speedLimitYData.Limit * directionMultiply;
                rbData.Value.velocity = velocity;
            }
        }
    }
}