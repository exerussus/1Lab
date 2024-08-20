
using Exerussus._1Extensions.Scripts.Extensions;
using Exerussus._1Lab.Scripts.ECS.Core;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class TagSystem : OneLabEcsListener<
        OneLabSignals.OnLabEntityInitializedSignal, 
        OneLabSignals.OnEcsMonoBehaviorStartDestroySignal, 
        OneLabSignals.CommandFilterTagSignal>
    {
        protected override void OnSignal(OneLabSignals.OnLabEntityInitializedSignal data)
        {
            foreach (var tag in data.OneLabEntity.tags)
            {
                TagsHandler.Add(tag, data.OneLabEntity.Entity);
            }
        }

        protected override void OnSignal(OneLabSignals.OnEcsMonoBehaviorStartDestroySignal data)
        {
            if (Pooler.Tags.Has(data.EcsMonoBehavior.Entity)) TagsHandler.Remove(data.EcsMonoBehavior.Entity);
        }

        protected override void OnSignal(OneLabSignals.CommandFilterTagSignal data)
        {
            var result = false;
            
            if (data.TagFilter.any.IsNotEmpty())
            {
                if (TagsHandler.HasAny(data.Entity, data.TagFilter.any))
                {
                    data.TagFilter.onSuccess?.Invoke(data.Entity, Componenter, Pooler);
                    return;
                }
            }
            
            if (data.TagFilter.include.IsNotEmpty())
            {
                var includeResult = TagsHandler.HasAll(data.Entity, data.TagFilter.include);
                if (includeResult) result = true;
            }
            
            if (result)
            {
                if (data.TagFilter.exclude.IsNotEmpty())
                {
                    result = !TagsHandler.HasAny(data.Entity, data.TagFilter.exclude);
                }
            }
            
            if (result) data.TagFilter.onSuccess?.Invoke(data.Entity, Componenter, Pooler);
        }
    }
}