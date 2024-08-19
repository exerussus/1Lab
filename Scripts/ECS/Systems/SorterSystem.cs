
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class SorterSystem : OneLabSystem
    {
        private const float BaseSort = 10f;
        private EcsFilter _sorterFilter;

        protected override void Initialize()
        {
            _sorterFilter = Componenter.Filter<OneLabData.SorterData>().End();
        }

        protected override void Update()
        {
            _sorterFilter.Foreach(OnSorterUpdate);
        }

        private void OnSorterUpdate(int entity)
        {
            ref var sorterData = ref Pooler.Sorter.Get(entity);
            SetSort(sorterData.Value.transform, sorterData.Value.ResultBottomPosition);
        }

        public static void SetSort(Transform transform, float bottomPosition)
        {
            var position = transform.position;
            position.z = BaseSort + bottomPosition * 0.01f;
            transform.position = position;
        }
    }
}