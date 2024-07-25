using _1Lab.Scripts.ECS.Core;
using _1Lab.Scripts.ECS.Systems;
using UnityEngine;

namespace _1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/CreateVfx")]
    public class CreateVfxEffect : EcsEffect
    {
        public GameObject vfxPrefab;
        public bool isLoop;
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale = Vector3.one;
        
        public void CreateInPosition()
        {
            OneLab.Signal.RegistryRaise(new CommandCreateVfxSignal
            {
                VfxPrefab = vfxPrefab,
                Position = position,
                Scale = scale,
                Rotation = rotation,
                IsLoop = isLoop
            });
        }

        public void CreateInPosition(Vector3 worldPosition)
        {
            OneLab.Signal.RegistryRaise(new CommandCreateVfxSignal
            {
                VfxPrefab = vfxPrefab,
                Position = worldPosition,
                Scale = scale,
                Rotation = rotation,
                IsLoop = isLoop
            });
        }

        public void CreateInOriginPosition(int originEntity, Componenter componenter)
        {
            ref var transformData = ref componenter.Get<TransformData>(originEntity);
            
            OneLab.Signal.RegistryRaise(new CommandCreateVfxSignal
            {
                VfxPrefab = vfxPrefab,
                Position = transformData.Value.position,
                Scale = scale,
                Rotation = rotation,
                IsLoop = isLoop
            });
        }

        public void CreateInOriginPosition(int originEntity, int targetEntity, Componenter componenter)
        {
            ref var transformData = ref componenter.Get<TransformData>(originEntity);
            
            OneLab.Signal.RegistryRaise(new CommandCreateVfxSignal
            {
                VfxPrefab = vfxPrefab,
                Position = transformData.Value.position,
                Scale = scale,
                Rotation = rotation,
                IsLoop = isLoop
            });
        }

        public void CreateInTargetPosition(int originEntity, int targetEntity, Componenter componenter)
        {
            ref var transformData = ref componenter.Get<TransformData>(targetEntity);
            
            OneLab.Signal.RegistryRaise(new CommandCreateVfxSignal
            {
                VfxPrefab = vfxPrefab,
                Position = transformData.Value.position,
                Scale = scale,
                Rotation = rotation,
                IsLoop = isLoop
            });
        }

        public void CreateBetweenPositions(int originEntity, int targetEntity, Componenter componenter)
        {
            ref var firstTransformData = ref componenter.Get<TransformData>(originEntity);
            ref var secondTransformData = ref componenter.Get<TransformData>(targetEntity);

            var betweenPosition = Vector3.Lerp(firstTransformData.Value.position, secondTransformData.Value.position, 0.5f);
            
            OneLab.Signal.RegistryRaise(new CommandCreateVfxSignal
            {
                VfxPrefab = vfxPrefab,
                Position = betweenPosition,
                Scale = scale,
                Rotation = rotation,
                IsLoop = isLoop
            });
        }

        public void CreateBetweenPositions(Vector3 firstPosition, Vector3 secondPosition)
        {
            var betweenPosition = Vector3.Lerp(firstPosition, secondPosition, 0.5f);
            
            OneLab.Signal.RegistryRaise(new CommandCreateVfxSignal
            {
                VfxPrefab = vfxPrefab,
                Position = betweenPosition,
                Scale = scale,
                Rotation = rotation,
                IsLoop = isLoop
            });
        }
    }
}