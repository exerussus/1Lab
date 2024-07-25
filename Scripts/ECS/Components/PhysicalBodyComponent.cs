
using _1Lab.Scripts.ECS.Core.Interfaces;
using _1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace _1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/PhysicalBody")]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicalBodyComponent : EcsComponent
    {
        [SerializeField, HideInInspector] private Rigidbody2D _rigidbody2D;

        public Rigidbody2D Rigidbody2D => _rigidbody2D;

        public override void Initialize()
        {
            ref var physicalBodyData = ref Componenter.Add<PhysicalBodyData>(Entity);
            physicalBodyData.Rigidbody2D = _rigidbody2D;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (_rigidbody2D == null) _rigidbody2D = GetComponent<Rigidbody2D>();
        }
    }

    public struct PhysicalBodyData : IEcsComponent
    {
        public Rigidbody2D Rigidbody2D;
    }
}