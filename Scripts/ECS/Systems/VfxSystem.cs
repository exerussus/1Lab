﻿
using Exerussus._1Lab.Scripts.ECS.Components;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using UnityEngine;
using System.Collections.Generic;
using Exerussus._1Extensions.Pools;
using Exerussus._1Lab.Scripts.ECS.Core;
using Object = UnityEngine.Object;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class VfxSystem : OneLabEcsListener<OneLabSignals.CommandCreateVfxSignal>
    {
        private EcsFilter _vfxFilter;
        private EcsFilter _vfxReleaseCommandFilter;
        private readonly Dictionary<GameObject, ObjectPool<VfxComponent>> _pools = new();

        protected override void Initialize()
        {
            _vfxFilter = Componenter.Filter<OneLabData.Vfx>().End();
            _vfxReleaseCommandFilter = Componenter.Filter<OneLabData.Vfx>().Inc<OneLabData.RequestReleaseVfxMark>().End();
        }

        protected override void Update()
        {
            _vfxFilter.Foreach(OnVfxUpdate);
            _vfxReleaseCommandFilter.Foreach(OnReleaseCommand);
        }

        private void OnReleaseCommand(int entity)
        {
            Pooler.CommandReleaseVfx.Del(entity);
            ReleaseVfx(entity);
        }

        private void ReleaseVfx(int entity)
        {
            ref var vfxData = ref Pooler.Vfx.Get(entity);

            if (vfxData.Value.prefab == null || !_pools.TryGetValue(vfxData.Value.prefab, out ObjectPool<VfxComponent> pool)) 
                Object.Destroy(vfxData.Value.gameObject);
            else
            {
                vfxData.Value.OnRelease();
                pool.ReleaseObject(vfxData.Value);
            }
            Componenter.DelEntity(entity);
        }

        private void OnVfxUpdate(int entity)
        {
            ref var vfxData = ref Pooler.Vfx.Get(entity);

            vfxData.FramesRemaining -= 1;
            if (vfxData.FramesRemaining > 0) return;

            vfxData.FramesRemaining = vfxData.Value.frameDelay;
            vfxData.Value.currentSprite++;

            if (vfxData.Value.sprites.Length == vfxData.Value.currentSprite)
            {
                if (vfxData.Value.isLoop)
                {
                    vfxData.Value.currentSprite = 0;
                    vfxData.Value.spriteRenderer.sprite = vfxData.Value.sprites[vfxData.Value.currentSprite];
                }
                else ReleaseVfx(entity);
            }
            else vfxData.Value.spriteRenderer.sprite = vfxData.Value.sprites[vfxData.Value.currentSprite];
        }

        protected override void OnSignal(OneLabSignals.CommandCreateVfxSignal data)
        {
            if (!_pools.ContainsKey(data.VfxPrefab))
            {
                var newPool = new ObjectPool<VfxComponent>(data.VfxPrefab);
                _pools[data.VfxPrefab] = newPool;
            }

            var pool = _pools[data.VfxPrefab];
            var vfx = pool.GetObject(data.Position, data.Scale, data.Rotation);
            vfx.isLoop = data.IsLoop;
            vfx.prefab = data.VfxPrefab;

            var newEntity = Componenter.GetNewEntity();
            ref var dataVfx = ref Pooler.Vfx.AddOrGet(newEntity);
            dataVfx.Value = vfx;
            dataVfx.FramesRemaining = vfx.frameDelay;
            dataVfx.LoopTimeRemaining = vfx.loopTime;
            vfx.Run();
        }
    }
}