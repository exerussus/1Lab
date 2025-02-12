﻿
using Exerussus._1Lab.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using Exerussus._1Lab.Scripts.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/Spawn")]
    public class SpawnEffect : EcsEffect
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Vector2 spawnPosition;
        public UnityEvent<int, OneLabPooler> onSpawn;
        
        public void SpawnInBetweenTargetPosition(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            if (!Activated) return;
            var originPosition = pooler.GetVector2Position(originEntity);
            var targetPosition = pooler.GetVector2Position(targetEntity);
            var resultPosition = Vector2.Lerp(originPosition, targetPosition, 0.5f);        
            Spawn(resultPosition);
        }
        
        public void SpawnInTargetPosition(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            if (!Activated) return;
            var targetPosition = pooler.GetVector2Position(targetEntity);
            Spawn(targetPosition);
        }
        
        public void SpawnInOriginPosition(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            if (!Activated) return;
            var originPosition = pooler.GetVector2Position(originEntity);
            Spawn(originPosition);
        }
        
        public void SpawnInOriginPosition(int originEntity, OneLabPooler pooler)
        {
            if (!Activated) return;
            var originPosition = pooler.GetVector2Position(originEntity);
            Spawn(originPosition);
        }

        public void Spawn()
        {
            var newObject = Instantiate(prefab, rotation: Quaternion.identity, position: spawnPosition);
            if (newObject.TryGetComponent(out EcsMonoBehavior ecsMonoBehavior))
            {
                ecsMonoBehavior.onInitialized += () =>
                {
                    onSpawn?.Invoke(ecsMonoBehavior.Entity, ecsMonoBehavior.Pooler);
                };
            }
        }

        private void Spawn(Vector2 position)
        {
            
            var newObject = Instantiate(prefab, rotation: Quaternion.identity, position: position);
            if (newObject.TryGetComponent(out EcsMonoBehavior ecsMonoBehavior))
            {
                ecsMonoBehavior.onInitialized += () =>
                {
                    onSpawn?.Invoke(ecsMonoBehavior.Entity, ecsMonoBehavior.Pooler);
                };
            }
        }
    }
    
}