
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using Exerussus._1Lab.Scripts.ECS.Effects;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Filters
{
    [AddComponentMenu("1Lab/Filter/TagFilter")]
    public class TagFilter : EcsEffect
    {
        public string[] any;
        public string[] include;
        public string[] exclude;
        public UnityEvent<int, Componenter, OneLabPooler> onSuccess;
        
        public void FilterOrigin(int originEntity, Componenter componenter, OneLabPooler pooler)
        {
            FilterProcess(originEntity, componenter, pooler);
        }
        
        public void FilterOrigin(int originEntity, int targetEntity, Componenter componenter, OneLabPooler pooler)
        {
            FilterProcess(originEntity, componenter, pooler);
        }
        
        public void FilterTarget(int originEntity, int targetEntity, Componenter componenter, OneLabPooler pooler)
        {
            FilterProcess(targetEntity, componenter, pooler);
        }

        private void FilterProcess(int entity, Componenter componenter, OneLabPooler pooler)
        {
            Signal.RegistryRaise(new OneLabSignals.CommandFilterTagSignal
            {
                Entity = entity,
                TagFilter = this
            });
        }
    }
}