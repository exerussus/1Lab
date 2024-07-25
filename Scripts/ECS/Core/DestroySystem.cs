using System;
using Leopotam.EcsLite;
using _1Lab.Scripts.SignalSystem;
using UnityEngine.Scripting;
using Object = UnityEngine.Object;

namespace _1Lab.Scripts.ECS.Core
{
    [Preserve]
    public class DestroySystem : EcsSignalListener<CommandKillEntitySignal>
    {
        private bool _toDestroy = true;
        private EcsFilter _destroyingFilter;
        
        protected override void OnSignal(CommandKillEntitySignal data)
        {
            var delay = data.Immediately ? 0.05f : OneLabConfiguration.Systems.destroy.entityDestroyDelay;
            Componenter.Get<EcsMonoBehaviorData>(data.Entity).Value.DestroyEcsMonoBehavior(delay);
        }

        protected override void Initialize()
        {
            _destroyingFilter = World.Filter<OnDestroyData>().End();
        }

        protected override void Update()
        {
            foreach (var entity in _destroyingFilter)
            {
                ref var onDestroyData = ref Componenter.Get<OnDestroyData>(entity);
                onDestroyData.TimeRemaining -= DeltaTime;
                
                if (onDestroyData.TimeRemaining <= 0)
                {
                    if (onDestroyData.ObjectToDelete != null)
                    {
                        if (!_toDestroy) onDestroyData.ObjectToDelete.gameObject.SetActive(false);
                        else Object.Destroy(onDestroyData.ObjectToDelete.gameObject);
                        if (Componenter.TryGetReadOnly(entity, out EcsMonoBehaviorData ecsMonoBehaviorData))
                        {
                            RegistrySignal(new OnEcsMonoBehaviorDestroyedSignal {EcsMonoBehavior = ecsMonoBehaviorData.Value});
                        }
                    }
                    Componenter.DelEntity(entity);
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