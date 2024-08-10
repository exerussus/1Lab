using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Components;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/CameraEffect")]
    public class CameraEffect : EcsEffect
    {
        [SerializeField] private Vector2 offset;
        
        public void FollowOrigin(int originEntity, Componenter<IOneLabEcsData> componenter)
        {
            Signal.RegistryRaise( new CommandCameraFollowTransformSignal
            {
                TargetTransform = componenter.Get<TransformData>(originEntity).Value,
                Offset = offset
            });
        }
        
        public void FollowOrigin(int originEntity, int targetEntity, Componenter<IOneLabEcsData> componenter)
        {
            Signal.RegistryRaise( new CommandCameraFollowTransformSignal {
                TargetTransform = componenter.Get<TransformData>(originEntity).Value,
                Offset = offset});
        }
        
        public void FollowTarget(int originEntity, int targetEntity, Componenter<IOneLabEcsData> componenter)
        {
            Signal.RegistryRaise( new CommandCameraFollowTransformSignal {
                TargetTransform = componenter.Get<TransformData>(targetEntity).Value,
                Offset = offset});
        }
    }
}