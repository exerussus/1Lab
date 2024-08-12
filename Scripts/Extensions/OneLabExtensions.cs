using Exerussus._1Extensions;
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
    }
}