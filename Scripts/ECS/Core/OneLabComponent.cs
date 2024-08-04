using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.Scripts.Extensions;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    [RequireComponent(typeof(OneLabEntity))]
    public abstract class OneLabComponent : EcsComponent
    {
        [SerializeField, HideInInspector] private OneLabEntity oneLabEntity;

        public OneLabEntity OneLabEntity => oneLabEntity;

        protected override void OnValidate()
        {
            base.OnValidate();
            oneLabEntity = gameObject.TryGetIfNull(ref oneLabEntity);
        }
    }
}