using System;
using Exerussus._1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using UnityEngine.Scripting;
using Object = UnityEngine.Object;

namespace Exerussus._1Lab.Scripts.Core
{
    [Preserve]
    public class DestroySystem : OneLabEcsListener<OneLabSignals.CommandKillEntitySignal>
    {
        private readonly bool _toDestroy = true;
        private EcsFilter _destroyingFilter;
        private const float DestroyDelay = 1f;
        
        protected override void OnSignal(OneLabSignals.CommandKillEntitySignal data)
        {
            var delay = data.Immediately ? 0.05f : DestroyDelay;
            Pooler.EcsMonoBehavior.Get(data.Entity).Value.DestroyEcsMonoBehavior(delay);
        }

        protected override void Initialize()
        {
            _destroyingFilter = World.Filter<OneLabData.OnDestroy>().End();
        }

        protected override void Update()
        {
            foreach (var entity in _destroyingFilter)
            {
                ref var onDestroyData = ref Pooler.OnDestroy.Get(entity);
                onDestroyData.TimeRemaining -= DeltaTime;
                
                if (onDestroyData.TimeRemaining <= 0)
                {
                    if (onDestroyData.ObjectToDelete != null)
                    {
                        if (!_toDestroy) onDestroyData.ObjectToDelete.gameObject.SetActive(false);
                        else Object.Destroy(onDestroyData.ObjectToDelete.gameObject);

                        if (Pooler.EcsMonoBehavior.Has(entity))
                        {
                            ref var ecsMonoBehaviorData = ref Pooler.EcsMonoBehavior.Get(entity);
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