
using Exerussus._1Lab.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Components.Clickables
{
    [AddComponentMenu("1Lab/Components/Clickable")]
    [DisallowMultipleComponent, RequireComponent(typeof(Collider2D))]
    public class ClickableComponent : EcsComponent
    {
        public UnityEvent<int, OneLabPooler> onMouseDown;

        protected virtual void OnMouseDown()
        {
            onMouseDown?.Invoke(Entity, Pooler);
        }
    }
}