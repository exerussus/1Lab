
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Components.Clickables
{
    [AddComponentMenu("1Lab/Components/Flickable")]
    [RequireComponent(typeof(Rigidbody2D))]
    public class FlickableComponent : ClickableComponent
    {
        [SerializeField] private bool toMouse;
        [SerializeField] private bool includeMass;
        [SerializeField] private float powerMultiply = 50;
        [SerializeField] private float maxDistance = 3f;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        private Camera _camera;
        private bool _isHolding;
        private float _distance;
        private Vector2 _direction;
        private const float PowerMultiply = 0.01f;
        private const float MassMultiply = 1f;
        
        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnMouseDrag()
        {
            _isHolding = true;
            var mousePosition = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
            var objectPosition = (Vector2)transform.position;
            _distance = Vector2.Distance(objectPosition, mousePosition);
            _distance = Mathf.Min(_distance, maxDistance);
            _direction = toMouse ? (mousePosition - objectPosition).normalized : (objectPosition - mousePosition).normalized;
        }

        private void Update()
        {
            if (!_isHolding || Input.GetMouseButton(0)) return;
    
            _isHolding = false;
    
            float massInfluence = includeMass ? 1 / (_rigidbody2D.mass + 1) : 1;
            massInfluence = Mathf.Max(massInfluence, 0.3f);
            var resultSpeed = _distance * PowerMultiply * powerMultiply * massInfluence;
    
            _rigidbody2D.velocity = resultSpeed * _direction;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (_rigidbody2D == null) _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }
}