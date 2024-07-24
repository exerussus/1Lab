using OneLab.Scripts.ECS.Core;
using Source.Scripts.ECS.Components;
using UnityEngine;

namespace Source.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/CameraEffect")]
    public class CameraEffect : EcsEffect
    {
        [SerializeField] private Vector2 offset;
        
        public void FollowOrigin(int originEntity, Componenter componenter)
        {
            Signal.RegistryRaise( new CommandCameraFollowTransformSignal
            {
                TargetTransform = componenter.Get<TransformData>(originEntity).Value,
                Offset = offset
            });
        }
        
        public void FollowOrigin(int originEntity, int targetEntity, Componenter componenter)
        {
            Signal.RegistryRaise( new CommandCameraFollowTransformSignal {
                TargetTransform = componenter.Get<TransformData>(originEntity).Value,
                Offset = offset});
        }
        
        public void FollowTarget(int originEntity, int targetEntity, Componenter componenter)
        {
            Signal.RegistryRaise( new CommandCameraFollowTransformSignal {
                TargetTransform = componenter.Get<TransformData>(targetEntity).Value,
                Offset = offset});
        }
    }
}