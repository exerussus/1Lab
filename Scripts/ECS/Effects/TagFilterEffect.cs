
using Exerussus._1Lab.Scripts.ECS.Core;
using Exerussus._1Lab.Scripts.ECS.Effects;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Filters
{
    [AddComponentMenu("1Lab/Effects/TagFilter")]
    public class TagFilterEffect : EcsEffect
    {
        public string[] any;
        public string[] include;
        public string[] exclude;
        public UnityEvent<int, OneLabPooler> onSuccess;
        
        public void FilterOrigin(int originEntity, OneLabPooler pooler)
        {
            FilterProcess(originEntity, pooler);
        }
        
        public void FilterOrigin(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            FilterProcess(originEntity, pooler);
        }
        
        public void FilterTarget(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            FilterProcess(targetEntity, pooler);
        }

        private void FilterProcess(int entity, OneLabPooler pooler)
        {
            Signal.RegistryRaise(new OneLabSignals.CommandFilterTagSignal
            {
                Entity = entity,
                TagFilter = this
            });
        }
    }
}