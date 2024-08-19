
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.Extensions
{
    public static class OneLabExtensions
    {
        public const string ParentFolder = "1Lab";
        
        public static T TryGetLabConfig<T>(this T component)
            where T : ScriptableObject
        {
            if (component == null)
            {
                component = ConfigLoader.GetOrCreate<T>(ParentFolder);
            }

            return component;
        }
        public static float GetDistance<TData>(this Componenter componenter, int firstEntity, int secondEntity) where TData : IEcsComponent
        {
            var firstTransform = componenter.Get<OneLabData.TransformData>(firstEntity).Value;
            var secondTransform = componenter.Get<OneLabData.TransformData>(secondEntity).Value;

            return Vector2.Distance(firstTransform.position, secondTransform.position);
        }

        public static bool GetIsDistanceLessThan<TData>(this Componenter componenter, int firstEntity, int secondEntity, float distance) where TData : IEcsComponent
        {
            var firstTransform = componenter.Get<OneLabData.TransformData>(firstEntity).Value;
            var secondTransform = componenter.Get<OneLabData.TransformData>(secondEntity).Value;

            return Vector2.Distance(firstTransform.position, secondTransform.position) > distance;
        }
        
        public static Vector2 GetVector2Position(this Componenter componenter, int entity)
        {
            ref var transformData = ref componenter.Get<OneLabData.TransformData>(entity);
            return transformData.Value.position;
        }
    }
}