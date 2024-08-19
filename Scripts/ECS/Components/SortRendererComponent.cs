
using Exerussus._1Lab.Scripts.ECS.Core;
using Exerussus._1Lab.Scripts.ECS.Systems;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/SortRenderer")]
    public class SortRendererComponent : OneLabComponent
    {
        [SerializeField] private bool autoRun = true;
        [SerializeField] private bool isDynamic = true;
        [SerializeField] private float bottomPosition;
        public float ResultBottomPosition => transform.position.y + bottomPosition;

        public override void Initialize()
        {
            if (autoRun) Run();
        }

        public void Run()
        {
            ref var sorterData = ref Pooler.Sorter.AddOrGet(Entity);
            sorterData.Value = this;
        }

        public void Stop()
        {
            Pooler.Sorter.Del(Entity);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            SorterSystem.SetSort(transform, ResultBottomPosition);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(new Vector3(transform.position.x + 2f, ResultBottomPosition), new Vector3(transform.position.x - 2f, ResultBottomPosition));
        }
    }
}