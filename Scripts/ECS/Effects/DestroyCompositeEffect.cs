
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    public class DestroyCompositeEffect : EcsEffect
    {
        public void DestroyCompositeToOrigin(int originEntity, OneLabPooler pooler)
        {
            if (!pooler.CompositeObject.Has(originEntity)) return;
            ref var compositeData = ref pooler.CompositeObject.Get(originEntity);
            compositeData.Value.DestroyComposite();
        }
        
        public void DestroyCompositeToOrigin(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            if (!pooler.CompositeObject.Has(originEntity)) return;
            ref var compositeData = ref pooler.CompositeObject.Get(originEntity);
            compositeData.Value.DestroyComposite();
        }
        
        public void DestroyCompositeToTarget(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            if (!pooler.CompositeObject.Has(targetEntity)) return;
            ref var compositeData = ref pooler.CompositeObject.Get(targetEntity);
            compositeData.Value.DestroyComposite();
        }
    }
}