using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Components.Clickables
{
    [AddComponentMenu("1Lab/Components/Clickable")]
    [DisallowMultipleComponent, RequireComponent(typeof(Collider2D))]
    public class ClickableComponent : EcsComponent<IOneLabEcsData>
    {
        public UnityEvent<int, Componenter<IOneLabEcsData>> onMouseDown;

        protected virtual void OnMouseDown()
        {
            onMouseDown?.Invoke(Entity, Componenter);
        }
    }
}