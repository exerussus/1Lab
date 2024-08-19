﻿
using Exerussus._1Extensions.Scripts.Extensions;
using Exerussus._1Lab.Scripts.ECS.Core;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class TagSystem : OneLabEcsListener<
        OneLabSignals.OnLabEntityInitializedSignal, 
        OneLabSignals.OnEcsMonoBehaviorStartDestroySignal, 
        OneLabSignals.CommandFilterTagSignal>
    {
        private TagsHandler _tagsHandler;

        protected override void Initialize()
        {
            GameShare.GetSharedObject(ref _tagsHandler);
        }

        protected override void OnSignal(OneLabSignals.OnLabEntityInitializedSignal data)
        {
            foreach (var tag in data.OneLabEntity.tags)
            {
                _tagsHandler.Add(tag, data.OneLabEntity.Entity);
            }
        }

        protected override void OnSignal(OneLabSignals.OnEcsMonoBehaviorStartDestroySignal data)
        {
            if (Pooler.Tags.Has(data.EcsMonoBehavior.Entity)) _tagsHandler.Remove(data.EcsMonoBehavior.Entity);
        }

        protected override void OnSignal(OneLabSignals.CommandFilterTagSignal data)
        {
            var result = false;
            
            if (data.TagFilter.any.IsNotEmpty())
            {
                if (_tagsHandler.HasAny(data.Entity, data.TagFilter.any))
                {
                    data.TagFilter.onSuccess?.Invoke(data.Entity, Componenter, Pooler);
                    return;
                }
            }
            
            if (data.TagFilter.include.IsNotEmpty())
            {
                var includeResult = _tagsHandler.HasAll(data.Entity, data.TagFilter.include);
                if (includeResult) result = true;
            }
            
            if (result)
            {
                if (data.TagFilter.exclude.IsNotEmpty())
                {
                    result = !_tagsHandler.HasAny(data.Entity, data.TagFilter.exclude);
                }
            }
            
            if (result) data.TagFilter.onSuccess?.Invoke(data.Entity, Componenter, Pooler);
        }
    }
}