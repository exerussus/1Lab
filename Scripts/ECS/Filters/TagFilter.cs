
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
        public UnityEvent<int, Componenter> onSuccess;
        
        public void FilterOrigin(int originEntity, Componenter componenter)
        {
            FilterProcess(originEntity, componenter);
        }
        
        public void FilterOrigin(int originEntity, int targetEntity, Componenter componenter)
        {
            FilterProcess(originEntity, componenter);
        }
        
        public void FilterTarget(int originEntity, int targetEntity, Componenter componenter)
        {
            FilterProcess(targetEntity, componenter);
        }

        private void FilterProcess(int entity, Componenter componenter)
        {
            Signal.RegistryRaise(new CommandFilterTagSignal
            {
                Entity = entity,
                TagFilter = this
            });
        }
    }

    public struct CommandFilterTagSignal
    {
        public TagFilter TagFilter;
        public int Entity;
    }
}