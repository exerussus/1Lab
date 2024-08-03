using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using Plugins.Exerussus._1Lab.Scripts.ECS.Core;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class TagSystem : EcsSignalListener<OnLabEntityInitializedSignal, OnEcsMonoBehaviorStartDestroySignal>
    {
        private TagsHandler _tagsHandler;
        
        protected override void Initialize()
        {
            _tagsHandler = GameShare.GetSharedObject<TagsHandler>();
        }

        protected override void OnSignal(OnLabEntityInitializedSignal data)
        {
            foreach (var tag in data.OneLabEntity.tags.values) _tagsHandler.Add(tag, data.OneLabEntity.Entity);
        }

        protected override void OnSignal(OnEcsMonoBehaviorStartDestroySignal data)
        {
            if (Componenter.Has<OneLabEntityData>(data.EcsMonoBehavior.Entity)) _tagsHandler.Remove(data.EcsMonoBehavior.Entity);
        }
    }
}