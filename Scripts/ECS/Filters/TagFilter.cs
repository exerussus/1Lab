using System.Linq;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.Scripts.Extensions;
using Exerussus._1Lab.Scripts.ECS.Components;
using Exerussus._1Lab.Scripts.ECS.Effects;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Filters
{
    [AddComponentMenu("1Lab/Filter/TagFilter")]
    public class TagFilter : EcsEffect
    {
        [SerializeField] private string[] any;
        [SerializeField] private string[] include;
        [SerializeField] private string[] exclude;
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
            if (componenter.Has<TagsData>(entity))
            {
                ref var tagsData = ref componenter.Get<TagsData>(entity);
                var result = false;
                
                if (any is { Length: > 0 })
                {
                    if (tagsData.Tags.ContainsAny(any))
                    {
                        onSuccess?.Invoke(entity, componenter);
                        return;
                    }
                }

                if (include is { Length: > 0 })
                {
                    var includeResult = true;
                    foreach (var lookingTag in include)
                    {
                        if (!tagsData.Tags.Contains(lookingTag))
                        {
                            includeResult = false;
                            break;
                        }
                    }

                    if (includeResult) result = true;
                }

                if (result)
                {
                    if (exclude is { Length: > 0 })
                    {
                        result = !exclude.ContainsAny(tagsData.Tags);
                    }
                }

                if (result) onSuccess?.Invoke(entity, componenter);
            }
        }
    }
}