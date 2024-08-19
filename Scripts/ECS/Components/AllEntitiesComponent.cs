using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/AllEntities")]
    public class AllEntitiesComponent : OneLabComponent
    {
        private EcsFilter _tagsFilter;
        public UnityEvent<int, Componenter> onExist;
        
        public override void Initialize()
        {
            _tagsFilter = Componenter.Filter<OneLabData.TagsData>().End();
        }

        public void GetAllEntitiesAndExecute()
        {
            foreach (var entity in _tagsFilter) onExist?.Invoke(entity, Componenter);
        }
    }
}