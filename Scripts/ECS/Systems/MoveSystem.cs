using OneLab.Scripts.ECS.Core;
using Source.Scripts.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Source.Scripts.ECS.Systems
{
    public class MoveSystem : EasySystem
    {
        private EcsFilter _pointMoverFilter;
        protected override void Initialize()
        {
            _pointMoverFilter = Componenter.Filter<PointMoverData>().End();
        }

        protected override void Update()
        {
            _pointMoverFilter.Foreach(OnPointMoveUpdate);
        }

        private void OnPointMoveUpdate(int entity)
        {
            ref var pointMoverData = ref Componenter.Get<PointMoverData>(entity);
            ref var transformData = ref Componenter.Get<TransformData>(entity);
            var currentPosition = (Vector2)transformData.Value.position;
            
            var targetPoint = pointMoverData.ToEndPoint ? pointMoverData.EndPoint : pointMoverData.StartPoint;
            var returnPoint = pointMoverData.ToEndPoint ? pointMoverData.StartPoint : pointMoverData.EndPoint;
            var step = Time.fixedDeltaTime * pointMoverData.Speed;
            var distance = Vector2.Distance(currentPosition, targetPoint);
            
            if (distance < 0.1f + step)
            {
                pointMoverData.ToEndPoint = !pointMoverData.ToEndPoint;
                targetPoint = returnPoint;
            }

            var resultStep = (targetPoint - currentPosition).normalized * step;
            transformData.Value.position += new Vector3(resultStep.x, resultStep.y, transformData.Value.position.z);
        }
    }
}