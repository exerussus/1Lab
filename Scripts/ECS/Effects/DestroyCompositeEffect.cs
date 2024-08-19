
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    public class DestroyCompositeEffect : EcsEffect
    {
        public void DestroyCompositeToOrigin(int originEntity, Componenter componenter, OneLabPooler pooler)
        {
            if (!componenter.Has<OneLabData.CompositeObjectData>(originEntity)) return;
            ref var compositeData = ref componenter.Get<OneLabData.CompositeObjectData>(originEntity);
            compositeData.Value.DestroyComposite();
        }
        
        public void DestroyCompositeToOrigin(int originEntity, int targetEntity, Componenter componenter, OneLabPooler pooler)
        {
            if (!componenter.Has<OneLabData.CompositeObjectData>(originEntity)) return;
            ref var compositeData = ref componenter.Get<OneLabData.CompositeObjectData>(originEntity);
            compositeData.Value.DestroyComposite();
        }
        
        public void DestroyCompositeToTarget(int originEntity, int targetEntity, Componenter componenter, OneLabPooler pooler)
        {
            if (!componenter.Has<OneLabData.CompositeObjectData>(targetEntity)) return;
            ref var compositeData = ref componenter.Get<OneLabData.CompositeObjectData>(targetEntity);
            compositeData.Value.DestroyComposite();
        }
    }
}