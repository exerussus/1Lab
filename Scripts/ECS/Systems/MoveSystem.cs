using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Exerussus._1Lab.Scripts.ECS.Components;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class MoveSystem : EasySystem<IOneLabEcsData>
    {
        private EcsFilter _pointMoverFilter;
        private Pooler _pooler;

        protected override void Initialize()
        {
            _pointMoverFilter = Componenter.Filter<PointMoverData>().End();
        }

        protected override void Update()
        {
            _pointMoverFilter.Foreach(OnPointMoveUpdate);
            _pooler = GameShare.GetSharedObject<Pooler>();
        }

        private void OnPointMoveUpdate(int entity)
        {
            ref var pointMoverData = ref _pooler.PointMover.Get(entity);
            ref var transformData = ref _pooler.Transform.Get(entity);
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