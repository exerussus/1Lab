
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Components.Clickables
{
    [AddComponentMenu("1Lab/Components/Dragable")]
    public class DragableComponent : ClickableComponent
    {
        [SerializeField] private bool _hasRigidbody;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnMouseDrag()
        {
            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = transform.position.z;
            transform.position = mousePosition;
            if (_hasRigidbody) _rigidbody2D.velocity = Vector2.zero;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (_rigidbody2D == null) _rigidbody2D = GetComponent<Rigidbody2D>();
            _hasRigidbody = _rigidbody2D != null;
        }
    }
}