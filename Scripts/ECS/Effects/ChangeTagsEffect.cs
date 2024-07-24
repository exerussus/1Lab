using OneLab.Scripts.ECS.Core;
using Source.Scripts.ECS.Components;
using UnityEngine;

namespace Source.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/ChangeTags")]
    public class ChangeTagsEffect : EcsEffect
    {
        [SerializeField] private string[] tags;

        public void SetTagsToOrigin(int originEntity, Componenter componenter)
        {
            ref var touchableData = ref componenter.Get<TouchableData>(originEntity);
            touchableData.Value.tags.Values = tags;
        }
        
        public void SetTagsToOrigin(int originEntity, int targetEntity, Componenter componenter)
        {
            ref var touchableData = ref componenter.Get<TouchableData>(originEntity);
            touchableData.Value.tags.Values = tags;
        }
        
        public void SetTagsToTarget(int originEntity, int targetEntity, Componenter componenter)
        {
            ref var touchableData = ref componenter.Get<TouchableData>(targetEntity);
            touchableData.Value.tags.Values = tags;
        }
        
        public void SetTargetTagsToOrigin(int originEntity, Componenter componenter)
        {
            ref var touchableData = ref componenter.Get<TouchableData>(originEntity);
            touchableData.Value.targetTags = tags;
        }
        
        public void SetTargetTagsToOrigin(int originEntity, int targetEntity, Componenter componenter)
        {
            ref var touchableData = ref componenter.Get<TouchableData>(originEntity);
            touchableData.Value.targetTags = tags;
        }
        
        public void SetTargetTagsToTarget(int originEntity, int targetEntity, Componenter componenter)
        {
            ref var touchableData = ref componenter.Get<TouchableData>(targetEntity);
            touchableData.Value.targetTags = tags;
        }
    }
}