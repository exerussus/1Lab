
using System;
using UnityEngine;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SignalSystem;
using Exerussus._1Lab.Scripts.ECS.Core;

namespace Exerussus._1Lab.Scripts.Core
{
    [SelectionBase]
    public abstract class EcsMonoBehavior : MonoBehaviour, IEcsMonoBehavior
    {
        #region SerializedFields
        
        [SerializeField, HideInInspector] private int entity;
        [SerializeField, HideInInspector] private bool isAlive = true;
        [SerializeField, HideInInspector] private bool isInitialized;
        [SerializeField, HideInInspector] private EcsComponent[] ecsComponents;
        [SerializeField, HideInInspector] private Rigidbody2D rb2D;
        [SerializeField, HideInInspector] private Rigidbody rb3D;
        
        #endregion

        #region Members
        
        public int Entity => entity;
        public bool IsAlive => isAlive;
        public Componenter Componenter { get; private set; }
        public OneLabPooler Pooler { get; private set; }
        public Signal Signal { get; private set; }
        public event Action onInitialized;

        #endregion

        #region InitAndDestroy

        public void Initialize(Componenter componenter, Signal signal, OneLabPooler pooler)
        {
            if (isInitialized) return;
            
            isInitialized = true;
            isAlive = true;
            Componenter = componenter;
            Pooler = pooler;
            Signal = signal;
            entity = Componenter.GetNewEntity();
            ref var transformData = ref Pooler.Transform.AddOrGet(entity);
            transformData.InitializeValues(transform);
            TryInitializePhysicalBody();
            foreach (var ecsComponent in ecsComponents) ecsComponent.PreInitialize(entity, Componenter, Pooler);
            foreach (var ecsComponent in ecsComponents) ecsComponent.Initialize();
            ref var ecsMonoBehData = ref Pooler.EcsMonoBehavior.AddOrGet(entity);
            ecsMonoBehData.InitializeValues(this);
            Signal.RegistryRaise(new OneLabSignals.OnEcsMonoBehaviorInitializedSignal { EcsMonoBehavior = this });
            onInitialized?.Invoke();
            onInitialized = null;
        }
        
        public void DestroyEcsMonoBehavior(float delay)
        {
            if (!IsAlive) return;
            isAlive = false;
            isInitialized = false;
            foreach (var ecsComponent in ecsComponents) ecsComponent.Destroy();
            Signal.RegistryRaise(new OneLabSignals.OnEcsMonoBehaviorStartDestroySignal { EcsMonoBehavior = this });
            ref var destroyingData = ref Pooler.OnDestroy.AddOrGet(entity);
            destroyingData.InitializeValues(gameObject, delay);
        }

        private void TryInitializePhysicalBody()
        {
            if (rb2D != null)
            {
                ref var physicalBodyData = ref Pooler.RigidBody2D.AddOrGet(Entity);
                physicalBodyData.Value = rb2D;
            }
            else if (rb3D != null)
            {
                ref var physicalBodyData = ref Pooler.RigidBody3D.AddOrGet(Entity);
                physicalBodyData.Value = rb3D;
            }
        }
        
        #endregion

        #region Methods

        public void SwitchActivated()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        #endregion

        #region Editor
        
#if UNITY_EDITOR

        private void OnValidate()
        {
            ecsComponents = GetComponents<EcsComponent>();
            rb2D = GetComponent<Rigidbody2D>();
            rb3D = GetComponent<Rigidbody>();
        }

#endif

        #endregion
        
        public interface IEcsComponentPreInitialize
        {
            public void PreInitialize(int entity, Componenter componenter);
        }
    
        public interface IEcsComponentInitialize : IEcsComponentPreInitialize
        {
            public void Initialize();
        }
    
        public interface IEcsComponentDestroy : IEcsComponentPreInitialize
        {
            public void Destroy();
        }
    }
    
    public interface IEcsMonoBehavior
    {
        public int Entity { get; }
        public void DestroyEcsMonoBehavior(float delay);
        public Transform transform { get; }
    }
}