using Leopotam.EcsLite;
using OneLab.Scripts.ECS.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/AllEntities")]
    public class AllEntitiesComponent : EcsComponent
    {
        private EcsFilter _tagsFilter;
        public UnityEvent<int, Componenter> onExist;
        
        public override void Initialize()
        {
            _tagsFilter = Componenter.Filter<TagsData>().End();
        }

        public void GetAllEntitiesAndExecute()
        {
            foreach (var entity in _tagsFilter) onExist?.Invoke(entity, Componenter);
        }
    }
}