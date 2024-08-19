using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Components;
using Exerussus._1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class SorterSystem : EasySystem
    {
        private const float BaseSort = 10f;
        private EcsFilter _sorterFilter;
        private OneLabPooler _pooler;

        protected override void Initialize()
        {
            _sorterFilter = Componenter.Filter<SorterData>().End();
            GameShare.GetSharedObject(ref _pooler);
        }

        protected override void Update()
        {
            _sorterFilter.Foreach(OnSorterUpdate);
        }

        private void OnSorterUpdate(int entity)
        {
            ref var sorterData = ref _pooler.Sorter.Get(entity);
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