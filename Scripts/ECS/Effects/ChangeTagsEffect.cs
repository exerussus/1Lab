
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/ChangeTags")]
    public class ChangeTagsEffect : EcsEffect
    {
        [SerializeField] private string[] tags;

        public void SetTagsToOrigin(int originEntity, OneLabPooler pooler)
        {
            ref var touchableData = ref pooler.Touchable.Get(originEntity);
            touchableData.Value.OneLabEntity.tags = tags;
        }
        
        public void SetTagsToOrigin(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            ref var touchableData = ref pooler.Touchable.Get(originEntity);
            touchableData.Value.OneLabEntity.tags = tags;
        }
        
        public void SetTagsToTarget(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            ref var touchableData = ref pooler.Touchable.Get(targetEntity);
            touchableData.Value.OneLabEntity.tags = tags;
        }
        
        public void SetTargetTagsToOrigin(int originEntity, OneLabPooler pooler)
        {
            ref var touchableData = ref pooler.Touchable.Get(originEntity);
            touchableData.Value.targetTags = tags;
        }
        
        public void SetTargetTagsToOrigin(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            ref var touchableData = ref pooler.Touchable.Get(originEntity);
            touchableData.Value.targetTags = tags;
        }
        
        public void SetTargetTagsToTarget(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            ref var touchableData = ref pooler.Touchable.Get(targetEntity);
            touchableData.Value.targetTags = tags;
        }
    }
}