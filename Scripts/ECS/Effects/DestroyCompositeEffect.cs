using _1Lab.Scripts.ECS.Core;
using _1Lab.Scripts.ECS.Components;

namespace _1Lab.Scripts.ECS.Effects
{
    public class DestroyCompositeEffect : EcsEffect
    {
        public void DestroyCompositeToOrigin(int originEntity, Componenter componenter)
        {
            if (!componenter.Has<CompositeObjectData>(originEntity)) return;
            ref var compositeData = ref componenter.Get<CompositeObjectData>(originEntity);
            compositeData.Value.DestroyComposite();
        }
        
        public void DestroyCompositeToOrigin(int originEntity, int targetEntity, Componenter componenter)
        {
            if (!componenter.Has<CompositeObjectData>(originEntity)) return;
            ref var compositeData = ref componenter.Get<CompositeObjectData>(originEntity);
            compositeData.Value.DestroyComposite();
        }
        
        public void DestroyCompositeToTarget(int originEntity, int targetEntity, Componenter componenter)
        {
            if (!componenter.Has<CompositeObjectData>(targetEntity)) return;
            ref var compositeData = ref componenter.Get<CompositeObjectData>(targetEntity);
            compositeData.Value.DestroyComposite();
        }
    }
}