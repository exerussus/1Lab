
using Source.Scripts.SignalSystem;
using Source.SignalSystem;
using UnityEngine;

namespace Source.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/SmartCamera")]
    public class SmartCameraComponent : MonoSignalListener<CommandCameraFollowTransformSignal, OnEcsMonoBehaviorStartDestroySignal>
    {
        [SerializeField] private bool hasTarget;
        [SerializeField] private Transform targetTransform;
        [SerializeField] private Vector2 offset;
        
        protected override void OnSignal(CommandCameraFollowTransformSignal data)
        {
            hasTarget = true;
            targetTransform = data.TargetTransform;
            offset = data.Offset;
        }

        protected override void OnSignal(OnEcsMonoBehaviorStartDestroySignal data)
        {
            if (!hasTarget) return;
            if (targetTransform.transform == data.EcsMonoBehavior.transform)
            {
                hasTarget = false;
                targetTransform = null;
            }
        }

        private void LateUpdate()
        {
            if (!hasTarget) return;

            var targetPosition = targetTransform.position;
            targetPosition.z = transform.position.z;
            targetPosition.x += offset.x;
            targetPosition.y += offset.y;
            transform.position = targetPosition;
        }
    }

    public struct CommandCameraFollowTransformSignal
    {
        public Transform TargetTransform;
        public Vector2 Offset;
    }
}