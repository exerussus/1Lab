
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
        public UnityEvent<int, OneLabPooler> onExist;
        
        public override void Initialize()
        {
            _tagsFilter = Componenter.Filter<OneLabData.Tags>().End();
        }

        public void GetAllEntitiesAndExecute()
        {
            foreach (var entity in _tagsFilter) onExist?.Invoke(entity, Pooler);
        }
    }
}