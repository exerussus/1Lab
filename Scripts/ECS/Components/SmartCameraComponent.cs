
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/SmartCamera")]
    public class SmartCameraComponent : OneLabComponent
    {
        [SerializeField] private bool autoStart = true;

        private void Start()
        {
            if (autoStart) Run();
        }

        public void Run()
        {
            ref var smartCameraData = ref Componenter.AddOrGet<SmartCameraData>(Entity);
            smartCameraData.Transform = transform;
        }

        public void Stop()
        {
            Componenter.Del<SmartCameraData>(Entity);
        }
    }

    public struct SmartCameraData : IOneLabEcsData
    {
        public Transform Transform;
    }

    public struct CommandCameraFollowTransformSignal
    {
        public bool FollowX;
        public bool FollowY;
        public int TargetEntity;
        public Vector2 Offset;
    }
}