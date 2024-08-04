
using System.Linq;
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Exerussus._1Lab.Scripts.ECS.Triggers;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class GlobalTriggerSystem : EcsSignalListener<CommandInvokeGlobalTrigger>
    {
        private EcsFilter _globalTriggerFilter;
        
        protected override void Initialize()
        {
            _globalTriggerFilter = Componenter.Filter<GlobalTriggerData>().End();
        }

        protected override void OnSignal(CommandInvokeGlobalTrigger data)
        {
            foreach (var entity in _globalTriggerFilter)
            {
                ref var globalTriggerData = ref Componenter.Get<GlobalTriggerData>(entity);
                if (globalTriggerData.Value.tags.Contains(data.Tag)) globalTriggerData.Value.onAction?.Invoke();
            }
        }
    }

    public struct CommandInvokeGlobalTrigger
    {
        public string Tag;
    }
}