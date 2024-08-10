using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Components;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/ChangeTags")]
    public class ChangeTagsEffect : EcsEffect
    {
        [SerializeField] private string[] tags;

        public void SetTagsToOrigin(int originEntity, Componenter<IOneLabEcsData> componenter)
        {
            ref var touchableData = ref componenter.Get<TouchableData>(originEntity);
            touchableData.Value.OneLabEntity.tags = tags;
        }
        
        public void SetTagsToOrigin(int originEntity, int targetEntity, Componenter<IOneLabEcsData> componenter)
        {
            ref var touchableData = ref componenter.Get<TouchableData>(originEntity);
            touchableData.Value.OneLabEntity.tags = tags;
        }
        
        public void SetTagsToTarget(int originEntity, int targetEntity, Componenter<IOneLabEcsData> componenter)
        {
            ref var touchableData = ref componenter.Get<TouchableData>(targetEntity);
            touchableData.Value.OneLabEntity.tags = tags;
        }
        
        public void SetTargetTagsToOrigin(int originEntity, Componenter<IOneLabEcsData> componenter)
        {
            ref var touchableData = ref componenter.Get<TouchableData>(originEntity);
            touchableData.Value.targetTags = tags;
        }
        
        public void SetTargetTagsToOrigin(int originEntity, int targetEntity, Componenter<IOneLabEcsData> componenter)
        {
            ref var touchableData = ref componenter.Get<TouchableData>(originEntity);
            touchableData.Value.targetTags = tags;
        }
        
        public void SetTargetTagsToTarget(int originEntity, int targetEntity, Componenter<IOneLabEcsData> componenter)
        {
            ref var touchableData = ref componenter.Get<TouchableData>(targetEntity);
            touchableData.Value.targetTags = tags;
        }
    }
}