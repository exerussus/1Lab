﻿
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/Destroy")]
    public class DestroyEffect : EcsEffect
    {
        [SerializeField] private bool immediately = true;
        
        public void DestroyTarget(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            if (!Activated) return;
            DestroyEntity(targetEntity);
        }
        
        public void DestroyOrigin(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            if (!Activated) return;
            DestroyEntity(originEntity);
        }
        
        public void DestroyOrigin(int originEntity, OneLabPooler pooler)
        {
            if (!Activated) return;
            DestroyEntity(originEntity);
        }

        private void DestroyEntity(int entity)
        {
            Signal.RegistryRaise(new OneLabSignals.CommandKillEntitySignal {Entity = entity, Immediately = immediately});
        }
    }
}