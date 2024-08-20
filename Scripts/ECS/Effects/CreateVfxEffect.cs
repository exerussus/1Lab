using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
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
            OneLab.Signal.RegistryRaise(new OneLabSignals.CommandCreateVfxSignal
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
            OneLab.Signal.RegistryRaise(new OneLabSignals.CommandCreateVfxSignal
            {
                VfxPrefab = vfxPrefab,
                Position = worldPosition,
                Scale = scale,
                Rotation = rotation,
                IsLoop = isLoop
            });
        }

        public void CreateInOriginPosition(int originEntity, Componenter componenter, OneLabPooler pooler)
        {
            ref var transformData = ref pooler.Transform.Get(originEntity);
            
            OneLab.Signal.RegistryRaise(new OneLabSignals.CommandCreateVfxSignal
            {
                VfxPrefab = vfxPrefab,
                Position = transformData.Value.position,
                Scale = scale,
                Rotation = rotation,
                IsLoop = isLoop
            });
        }

        public void CreateInOriginPosition(int originEntity, int targetEntity, Componenter componenter, OneLabPooler pooler)
        {
            ref var transformData = ref pooler.Transform.Get(originEntity);
            
            OneLab.Signal.RegistryRaise(new OneLabSignals.CommandCreateVfxSignal
            {
                VfxPrefab = vfxPrefab,
                Position = transformData.Value.position,
                Scale = scale,
                Rotation = rotation,
                IsLoop = isLoop
            });
        }

        public void CreateInTargetPosition(int originEntity, int targetEntity, Componenter componenter, OneLabPooler pooler)
        {
            ref var transformData = ref pooler.Transform.Get(targetEntity);
            
            OneLab.Signal.RegistryRaise(new OneLabSignals.CommandCreateVfxSignal
            {
                VfxPrefab = vfxPrefab,
                Position = transformData.Value.position,
                Scale = scale,
                Rotation = rotation,
                IsLoop = isLoop
            });
        }

        public void CreateBetweenPositions(int originEntity, int targetEntity, Componenter componenter, OneLabPooler pooler)
        {
            ref var firstTransformData = ref pooler.Transform.Get(originEntity);
            ref var secondTransformData = ref pooler.Transform.Get(targetEntity);

            var betweenPosition = Vector3.Lerp(firstTransformData.Value.position, secondTransformData.Value.position, 0.5f);
            
            OneLab.Signal.RegistryRaise(new OneLabSignals.CommandCreateVfxSignal
            {
                VfxPrefab = vfxPrefab,
                Position = betweenPosition,
                Scale = scale,
                Rotation = rotation,
                IsLoop = isLoop
            });
        }

        public void CreateBetweenPositions(Vector3 firstPosition, Vector3 secondPosition, OneLabPooler pooler)
        {
            var betweenPosition = Vector3.Lerp(firstPosition, secondPosition, 0.5f);
            
            OneLab.Signal.RegistryRaise(new OneLabSignals.CommandCreateVfxSignal
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