
using System.Linq;
using Exerussus._1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class GlobalTriggerSystem : OneLabEcsListener<OneLabSignals.CommandInvokeGlobalTrigger>
    {
        private EcsFilter _globalTriggerFilter;

        protected override void Initialize()
        {
            _globalTriggerFilter = Componenter.Filter<OneLabData.GlobalTrigger>().End();
        }

        protected override void OnSignal(OneLabSignals.CommandInvokeGlobalTrigger data)
        {
            foreach (var entity in _globalTriggerFilter)
            {
                ref var globalTriggerData = ref Pooler.GlobalTrigger.Get(entity);
                if (globalTriggerData.Value.tags.Contains(data.Tag)) globalTriggerData.Value.onAction?.Invoke();
            }
        }
    }
}