using System.Collections.Generic;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.Scripts.Extensions;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/CompositeObject")]
    public class CompositeObjectComponent : OneLabComponent
    {
        [Tooltip("Сила, которая будет воздействовать на вложенные объекты при уничтожении композитного объекта.")]
        [SerializeField] private float powerOnDestruct = 3f;
        [SerializeField] private GameObject[] objects;
        [SerializeField, HideInInspector] private Rigidbody2D[] _rigidBodies;

        public UnityEvent<int, Componenter, OneLabPooler> onDestroy;

        public override void Initialize()
        {
            ref var compositeObjectData = ref Pooler.CompositeObject.AddOrGet(Entity);
            compositeObjectData.Value = this;
        }

        public override void Destroy()
        {
            Pooler.CompositeObject.Del(Entity);
        }

        public void DestroyComposite()
        {
            foreach (var rb in _rigidBodies)
            {
                rb.transform.parent = transform.parent;
                rb.isKinematic = false;
                rb.velocity += new Vector2(Random.Range(-powerOnDestruct, powerOnDestruct), Random.Range(-powerOnDestruct, powerOnDestruct));
            }
            onDestroy?.Invoke(Entity, Componenter, Pooler);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (objects == null) return;
            var list = new List<Rigidbody2D>();
            foreach (var compositeObject in objects)
            {
                var rb = compositeObject.AddOrGet<Rigidbody2D>();
                list.Add(rb);
                rb.isKinematic = true;
            }
            _rigidBodies = list.ToArray();
        }
    }
}