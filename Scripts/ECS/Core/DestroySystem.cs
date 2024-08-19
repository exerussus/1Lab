using System;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using UnityEngine.Scripting;
using Object = UnityEngine.Object;

namespace Exerussus._1Lab.Scripts.Core
{
    [Preserve]
    public class DestroySystem : EcsSignalListener<OneLabSignals.CommandKillEntitySignal>
    {
        private readonly bool _toDestroy = true;
        private EcsFilter _destroyingFilter;
        private const float DestroyDelay = 1f;
        private EcsPool<OneLabData.EcsMonoBehaviorData> _monoBehPool;
        private EcsPool<OneLabData.OnDestroyData> _destroyPool;
        
        protected override void OnSignal(OneLabSignals.CommandKillEntitySignal data)
        {
            var delay = data.Immediately ? 0.05f : DestroyDelay;
            _monoBehPool.Get(data.Entity).Value.DestroyEcsMonoBehavior(delay);
        }

        protected override void Initialize()
        {
            _monoBehPool = World.GetPool<OneLabData.EcsMonoBehaviorData>();
            _destroyPool = World.GetPool<OneLabData.OnDestroyData>();
            _destroyingFilter = World.Filter<OneLabData.OnDestroyData>().End();
        }

        protected override void Update()
        {
            foreach (var entity in _destroyingFilter)
            {
                ref var onDestroyData = ref _destroyPool.Get(entity);
                onDestroyData.TimeRemaining -= DeltaTime;
                
                if (onDestroyData.TimeRemaining <= 0)
                {
                    if (onDestroyData.ObjectToDelete != null)
                    {
                        if (!_toDestroy) onDestroyData.ObjectToDelete.gameObject.SetActive(false);
                        else Object.Destroy(onDestroyData.ObjectToDelete.gameObject);

                        if (_monoBehPool.Has(entity))
                        {
                            ref var ecsMonoBehaviorData = ref _monoBehPool.Get(entity);
                            RegistrySignal(new OneLabSignals.OnEcsMonoBehaviorDestroyedSignal {EcsMonoBehavior = ecsMonoBehaviorData.Value});
                        }
                    }
                    World.DelEntity(entity);
                }
            }
        }
        
        [Serializable]
        public class Settings
        {
            public float entityDestroyDelay = 1.15f;
        }
    }
}