
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/DirectionMover")]
    public class DirectionMoverComponent : OneLabComponent
    {
        public bool autoRun = true;
        public Vector2 direction = Vector2.right;
        public float speed = 5;

        public override void Initialize()
        {
            if (autoRun) Run();
        }

        public void Run()
        {
            ref var directionMoverData = ref Pooler.DirectionMover.AddOrGet(Entity);
            directionMoverData.Value = this;
        }

        public void Stop()
        {
            Pooler.DirectionMover.Del(Entity);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)(direction.normalized * 2f));
        }
    }
}