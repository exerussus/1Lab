
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class SpeedLimitSystem : OneLabSystem
    {
        private EcsFilter _speedXLimitFilter;
        private EcsFilter _speedYLimitFilter;

        protected override void Initialize()
        {
            _speedXLimitFilter = Componenter.Filter<OneLabData.SpeedLimitXData>().End();
            _speedYLimitFilter = Componenter.Filter<OneLabData.SpeedLimitYData>().End();
        }

        protected override void Update()
        {
            _speedXLimitFilter.Foreach(OnLimitXUpdate);
            _speedYLimitFilter.Foreach(OnLimitYUpdate);
        }

        private void OnLimitXUpdate(int entity)
        {
            ref var speedLimitXData = ref Pooler.SpeedLimitX.Get(entity);
            ref var rbData = ref Pooler.RigidBody2D.Get(entity);
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
            ref var speedLimitYData = ref Pooler.SpeedLimitY.Get(entity);
            ref var rbData = ref Pooler.RigidBody2D.Get(entity);
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