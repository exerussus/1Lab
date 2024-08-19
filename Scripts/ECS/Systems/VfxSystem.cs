
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
    public class VfxSystem : EcsSignalListener<OneLabSignals.CommandCreateVfxSignal>
    {
        private EcsFilter _vfxFilter;
        private EcsFilter _vfxReleaseCommandFilter;
        private Dictionary<GameObject, ObjectPool<VfxComponent>> _pools = new();
        private OneLabPooler _pooler;

        protected override void Initialize()
        {
            _vfxFilter = Componenter.Filter<OneLabData.VfxData>().End();
            _vfxReleaseCommandFilter = Componenter.Filter<OneLabData.VfxData>().Inc<OneLabData.CommandReleaseVfxMark>().End();
            GameShare.GetSharedObject(ref _pooler);
        }

        protected override void Update()
        {
            _vfxFilter.Foreach(OnVfxUpdate);
            _vfxReleaseCommandFilter.Foreach(OnReleaseCommand);
        }

        private void OnReleaseCommand(int entity)
        {
            _pooler.CommandReleaseVfx.Del(entity);
            ReleaseVfx(entity);
        }

        private void ReleaseVfx(int entity)
        {
            ref var vfxData = ref _pooler.Vfx.Get(entity);

            if (vfxData.Vfx.prefab == null || !_pools.TryGetValue(vfxData.Vfx.prefab, out ObjectPool<VfxComponent> pool)) 
                Object.Destroy(vfxData.Vfx.gameObject);
            else
            {
                vfxData.Vfx.OnRelease();
                pool.ReleaseObject(vfxData.Vfx);
            }
            Componenter.DelEntity(entity);
        }

        private void OnVfxUpdate(int entity)
        {
            ref var vfxData = ref _pooler.Vfx.Get(entity);

            vfxData.FramesRemaining -= 1;
            if (vfxData.FramesRemaining > 0) return;

            vfxData.FramesRemaining = vfxData.Vfx.frameDelay;
            vfxData.Vfx.currentSprite++;

            if (vfxData.Vfx.sprites.Length == vfxData.Vfx.currentSprite)
            {
                if (vfxData.Vfx.isLoop)
                {
                    vfxData.Vfx.currentSprite = 0;
                    vfxData.Vfx.spriteRenderer.sprite = vfxData.Vfx.sprites[vfxData.Vfx.currentSprite];
                }
                else ReleaseVfx(entity);
            }
            else vfxData.Vfx.spriteRenderer.sprite = vfxData.Vfx.sprites[vfxData.Vfx.currentSprite];
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
            ref var dataVfx = ref _pooler.Vfx.AddOrGet(newEntity);
            dataVfx.Vfx = vfx;
            dataVfx.FramesRemaining = vfx.frameDelay;
            dataVfx.LoopTimeRemaining = vfx.loopTime;
            vfx.Run();
        }
    }
}