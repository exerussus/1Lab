
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/CameraTarget")]
    public class CameraTargetComponent : OneLabComponent
    {
        [SerializeField] private bool autoRun = true;
        [SerializeField] private bool followX = true;
        [SerializeField] private bool followY = true;
        [SerializeField] private Vector2 offset;
        
        public override void Initialize()
        {
            if (autoRun) Run();
        }

        public void Run()
        {
            Signal.RegistryRaise(new OneLabSignals.CommandCameraFollowTransformSignal
            {
                TargetEntity = Entity,
                FollowY = followY,
                FollowX = followX,
                Offset = offset
            });
        }

        public void Stop()
        {
            Componenter.Del<OneLabData.CameraTargetData>(Entity);
        }
    }
}