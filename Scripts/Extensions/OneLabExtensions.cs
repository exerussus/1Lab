
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
        public static float GetDistance(this OneLabPooler pooler, int firstEntity, int secondEntity)
        {
            var firstTransform = pooler.Transform.Get(firstEntity).Value;
            var secondTransform = pooler.Transform.Get(secondEntity).Value;

            return Vector2.Distance(firstTransform.position, secondTransform.position);
        }

        public static bool GetIsDistanceLessThan(this OneLabPooler pooler, int firstEntity, int secondEntity, float distance)
        {
            var firstTransform = pooler.Transform.Get(firstEntity).Value;
            var secondTransform = pooler.Transform.Get(secondEntity).Value;

            return Vector2.Distance(firstTransform.position, secondTransform.position) > distance;
        }
        
        public static Vector2 GetVector2Position(this OneLabPooler pooler, int entity)
        {
            ref var transformData = ref pooler.Transform.Get(entity);
            return transformData.Value.position;
        }
    }
}