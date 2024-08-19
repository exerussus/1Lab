﻿
using System.Linq;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using Exerussus._1Lab.Scripts.ECS.Triggers;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class GlobalTriggerSystem : EcsSignalListener<CommandInvokeGlobalTrigger>
    {
        private EcsFilter _globalTriggerFilter;
        private OneLabPooler _pooler;

        protected override void Initialize()
        {
            _globalTriggerFilter = Componenter.Filter<GlobalTriggerData>().End();
            GameShare.GetSharedObject(ref _pooler);
        }

        protected override void OnSignal(CommandInvokeGlobalTrigger data)
        {
            foreach (var entity in _globalTriggerFilter)
            {
                ref var globalTriggerData = ref _pooler.GlobalTrigger.Get(entity);
                if (globalTriggerData.Value.tags.Contains(data.Tag)) globalTriggerData.Value.onAction?.Invoke();
            }
        }
    }

    public struct CommandInvokeGlobalTrigger
    {
        public string Tag;
    }
}