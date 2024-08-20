
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class MoveSystem : OneLabSystem
    {
        private EcsFilter _pointMoverFilter;
        private EcsFilter _directionMoverFilter;

        protected override void Initialize()
        {
            _pointMoverFilter = Componenter.Filter<OneLabData.PointMover>().End();
            _directionMoverFilter = Componenter.Filter<OneLabData.DirectionMover>().Inc<OneLabData.Transform>().End();
        }

        protected override void Update()
        {
            _pointMoverFilter.Foreach(OnPointMoveUpdate);
            _directionMoverFilter.Foreach(OnDirectionMoveUpdate);
        }

        private void OnDirectionMoveUpdate(int entity)
        {
            ref var directionMoverData = ref Pooler.DirectionMover.Get(entity);
            ref var transformData = ref Pooler.Transform.Get(entity);
            
            transformData.Value.Translate(directionMoverData.Speed * DeltaTime * directionMoverData.Direction);
        }

        private void OnPointMoveUpdate(int entity)
        {
            ref var pointMoverData = ref Pooler.PointMover.Get(entity);
            ref var transformData = ref Pooler.Transform.Get(entity);
            var currentPosition = (Vector2)transformData.Value.position;
            
            var targetPoint = pointMoverData.ToEndPoint ? pointMoverData.EndPoint : pointMoverData.StartPoint;
            var returnPoint = pointMoverData.ToEndPoint ? pointMoverData.StartPoint : pointMoverData.EndPoint;
            var step = Time.deltaTime * pointMoverData.Speed;
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