
using _1Lab.Scripts.ECS.Core.Interfaces;
using _1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace _1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/PointMover")]
    public class PointMoverComponent : EcsComponent
    {
        [SerializeField] private float speed;
        [SerializeField] private Vector2 point;
        [SerializeField, HideInInspector] private Vector2 startPosition;
        private readonly Color _pointColor = Color.red;
        private readonly Color _lineColor = Color.green;

        public override void Initialize()
        {
            Run();
        }

        public override void Destroy()
        {
            Stop();
        }

        public void ChangeSpeed(float value)
        {
            speed = value;
            Run();
        }
        
        public void Run()
        {
            ref var pointMoverData = ref Componenter.AddOrGet<PointMoverData>(Entity);
            pointMoverData.Speed = speed;
            pointMoverData.StartPoint = startPosition;
            pointMoverData.EndPoint = (Vector2)transform.position + point;
            pointMoverData.ToEndPoint = true;
        }

        public void Stop()
        {
            Componenter.Del<PointMoverData>(Entity);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _pointColor;
            var resultTargetPoint = (Vector2)transform.position + point;
            Gizmos.DrawSphere(resultTargetPoint, 0.1f);
            Gizmos.DrawSphere(startPosition, 0.1f);
            Gizmos.color = _lineColor;
            Gizmos.DrawLine(startPosition, resultTargetPoint);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            startPosition = transform.position;
        }
    }

    public struct PointMoverData : IEcsComponent
    {
        public float Speed;
        public Vector2 StartPoint;
        public Vector2 EndPoint;
        public bool ToEndPoint;
    }
}