
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/CameraEffect")]
    public class CameraEffect : EcsEffect
    {
        [SerializeField] private Vector2 offset;
        [SerializeField] private bool followX;
        [SerializeField] private bool followY;
        
        public void FollowOrigin(int originEntity, OneLabPooler pooler)
        {
            Signal.RegistryRaise( new OneLabSignals.CommandCameraFollowTransformSignal
            {
                TargetEntity = originEntity,
                Offset = offset,
                FollowX = followX,
                FollowY = followY
            });
        }
        
        public void FollowOrigin(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            Signal.RegistryRaise( new OneLabSignals.CommandCameraFollowTransformSignal {
                TargetEntity = originEntity,
                Offset = offset,
                FollowX = followX,
                FollowY = followY
            });
        }
        
        public void FollowTarget(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            Signal.RegistryRaise( new OneLabSignals.CommandCameraFollowTransformSignal {
                TargetEntity = originEntity,
                Offset = offset,
                FollowX = followX,
                FollowY = followY});
        }
    }
}