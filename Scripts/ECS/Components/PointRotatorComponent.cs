
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/PointRotator")]
    public class PointRotatorComponent : OneLabComponent
    {
        [SerializeField] private float speed;
        [SerializeField] private Vector2 point;
        [SerializeField] private bool reversed;
        private readonly Color _pointColor = Color.red;
        private readonly Color _axisColor = Color.green;

        public void SetSpeed(float value)
        {
            speed = value;
            Run();
        }
        
        public void ChangeSpeed(float value)
        {
            speed += value;
            Run();
        }
        
        public override void Initialize()
        {
            Run();
        }

        public override void Destroy()
        {
            Stop();
        }

        public void Run()
        {
            ref var pointRotatorData = ref Componenter.AddOrGet<OneLabData.PointRotatorData>(Entity);
            pointRotatorData.Point = point;
            pointRotatorData.Speed = reversed? -speed : speed;
        }

        public void Stop()
        {
            Componenter.Del<OneLabData.PointRotatorData>(Entity);
        }

        public void Reverse()
        {
            reversed = !reversed;
            Run();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _pointColor;
            Gizmos.DrawSphere(point, 0.1f);
            Gizmos.color = _axisColor;
            Gizmos.DrawLine(transform.position, point);
            Gizmos.DrawWireSphere(point, Vector2.Distance(transform.position, point));
        }
    }
}