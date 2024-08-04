
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using Exerussus._1Lab.Scripts.ECS.Filters;
using Plugins.Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class TagSystem : EcsSignalListener<OnLabEntityInitializedSignal, OnEcsMonoBehaviorStartDestroySignal, CommandFilterTagSignal>
    {
        private TagsHandler _tagsHandler;
        
        protected override void Initialize()
        {
            _tagsHandler = GameShare.GetSharedObject<TagsHandler>();
        }

        protected override void OnSignal(OnLabEntityInitializedSignal data)
        {
            foreach (var tag in data.OneLabEntity.tags)
            {
                _tagsHandler.Add(tag, data.OneLabEntity.Entity);
            }
        }

        protected override void OnSignal(OnEcsMonoBehaviorStartDestroySignal data)
        {
            if (Componenter.Has<OneLabEntityData>(data.EcsMonoBehavior.Entity)) _tagsHandler.Remove(data.EcsMonoBehavior.Entity);
        }

        protected override void OnSignal(CommandFilterTagSignal data)
        {
            var result = false;
            
            if (data.TagFilter.any is { Length: > 0 })
            {
                if (_tagsHandler.HasAny(data.Entity, data.TagFilter.any))
                {
                    data.TagFilter.onSuccess?.Invoke(data.Entity, Componenter);
                    return;
                }
            }
            
            if (data.TagFilter.include is { Length: > 0 })
            {
                var includeResult = _tagsHandler.HasAll(data.Entity, data.TagFilter.include);
                if (includeResult) result = true;
            }
            
            if (result)
            {
                if (data.TagFilter.exclude is { Length: > 0 })
                {
                    result = !_tagsHandler.HasAny(data.Entity, data.TagFilter.exclude);
                }
            }
            
            if (result) data.TagFilter.onSuccess?.Invoke(data.Entity, Componenter);
        }
    }
}