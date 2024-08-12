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
        [SerializeField] private bool followX;
        [SerializeField] private bool followY;
        
        public void FollowOrigin(int originEntity, Componenter<IOneLabEcsData> componenter)
        {
            Signal.RegistryRaise( new CommandCameraFollowTransformSignal
            {
                TargetEntity = originEntity,
                Offset = offset,
                FollowX = followX,
                FollowY = followY
            });
        }
        
        public void FollowOrigin(int originEntity, int targetEntity, Componenter<IOneLabEcsData> componenter)
        {
            Signal.RegistryRaise( new CommandCameraFollowTransformSignal {
                TargetEntity = originEntity,
                Offset = offset,
                FollowX = followX,
                FollowY = followY
            });
        }
        
        public void FollowTarget(int originEntity, int targetEntity, Componenter<IOneLabEcsData> componenter)
        {
            Signal.RegistryRaise( new CommandCameraFollowTransformSignal {
                TargetEntity = originEntity,
                Offset = offset,
                FollowX = followX,
                FollowY = followY});
        }
    }
}