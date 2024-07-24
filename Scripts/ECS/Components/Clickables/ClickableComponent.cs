using OneLab.Scripts.ECS.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.ECS.Components.Clickables
{
    [AddComponentMenu("1Lab/Components/Clickable")]
    [DisallowMultipleComponent, RequireComponent(typeof(Collider2D))]
    public class ClickableComponent : EcsComponent
    {
        public UnityEvent<int, Componenter> onMouseDown;

        protected virtual void OnMouseDown()
        {
            onMouseDown?.Invoke(Entity, Componenter);
        }
    }
}