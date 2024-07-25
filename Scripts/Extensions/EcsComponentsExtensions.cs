using System.Collections.Generic;
using _1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace _1Lab.Scripts.Extensions
{
    public static class EcsComponentsExtensions
    {
        public static Vector2 GetVector2Position(this Componenter componenter, int entity)
        {
            ref var transformData = ref componenter.Get<TransformData>(entity);
            return (Vector2)transformData.Value.position;
        }

        public static IEcsSystems AddSystems(this IEcsSystems systems, List<IEcsSystem> addingSystems)
        {
            foreach (var system in addingSystems)
            {
                systems.Add(system);
            }

            return systems;
        }

        public static IEcsSystems AddSystems(this IEcsSystems systems, IEcsSystem[] addingSystems)
        {
            foreach (var system in addingSystems)
            {
                systems.Add(system);
            }

            return systems;
        }
    }
}