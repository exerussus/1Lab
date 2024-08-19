
using System;
using UnityEngine;
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SignalSystem;

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
        public Signal Signal { get; private set; }
        public event Action onInitialized;

        #endregion

        #region InitAndDestroy

        public void Initialize(Componenter componenter, Signal signal)
        {
            if (isInitialized) return;
            
            isInitialized = true;
            isAlive = true;
            Componenter = componenter;
            Signal = signal;
            entity = Componenter.GetNewEntity();
            ref var transformData = ref Componenter.AddOrGet<TransformData>(entity);
            transformData.InitializeValues(transform);
            TryInitializePhysicalBody();
            foreach (var ecsComponent in ecsComponents) ecsComponent.PreInitialize(entity, Componenter);
            foreach (var ecsComponent in ecsComponents) ecsComponent.Initialize();
            ref var ecsMonoBehData = ref Componenter.AddOrGet<EcsMonoBehaviorData>(entity);
            ecsMonoBehData.InitializeValues(this);
            Signal.RegistryRaise(new OnEcsMonoBehaviorInitializedSignal { EcsMonoBehavior = this });
            onInitialized?.Invoke();
            onInitialized = null;
        }
        
        public void DestroyEcsMonoBehavior(float delay)
        {
            if (!IsAlive) return;
            isAlive = false;
            isInitialized = false;
            foreach (var ecsComponent in ecsComponents) ecsComponent.Destroy();
            Signal.RegistryRaise(new OnEcsMonoBehaviorStartDestroySignal { EcsMonoBehavior = this });
            ref var destroyingData = ref Componenter.AddOrGet<OnDestroyData>(entity);
            destroyingData.InitializeValues(gameObject, delay);
        }

        private void TryInitializePhysicalBody()
        {
            if (rb2D != null)
            {
                ref var physicalBodyData = ref Componenter.AddOrGet<RigidBody2DData>(Entity);
                physicalBodyData.Value = rb2D;
            }
            else if (rb3D != null)
            {
                ref var physicalBodyData = ref Componenter.AddOrGet<RigidBody3DData>(Entity);
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

    public struct EcsMonoBehaviorData : IEcsComponent
    {
        public IEcsMonoBehavior Value;
        
        public void InitializeValues(IEcsMonoBehavior value)
        {
            Value = value;
        }
    }
    
    public interface IEcsMonoBehavior
    {
        public int Entity { get; }
        public void DestroyEcsMonoBehavior(float delay);
        public Transform transform { get; }
    }

    #region Data

    public struct TransformData : IEcsComponent
    {
        public Transform Value;
        public void InitializeValues(Transform value)
        {
            Value = value;
        }
    }

    public struct RigidBody2DData : IEcsComponent
    {
        public Rigidbody2D Value;
    }

    public struct RigidBody3DData : IEcsComponent
    {
        public Rigidbody Value;
    }
    
    public struct OnDestroyData : IEcsData<GameObject, float>
    {
        public float TimeRemaining;
        public GameObject ObjectToDelete;
        public void InitializeValues(GameObject objectToDelete, float value)
        {
            TimeRemaining = value;
            ObjectToDelete = objectToDelete;
        }
    }

    #endregion
    
    #region Signals

    /// <summary>
    /// Команда на уничтожение Entity 
    /// </summary>
    public struct CommandKillEntitySignal
    {
        public int Entity;
        public bool Immediately;
    }
    
    /// <summary>
    /// MonoBehaviourView проинициализировался.
    /// </summary>
    public struct OnEcsMonoBehaviorInitializedSignal
    {
        public IEcsMonoBehavior EcsMonoBehavior;
    }
    
    /// <summary>
    /// MonoBehaviourView уничтожился.
    /// </summary>
    public struct OnEcsMonoBehaviorStartDestroySignal
    {
        public IEcsMonoBehavior EcsMonoBehavior;
    }
    
    /// <summary>
    /// MonoBehaviourView уничтожился.
    /// </summary>
    public struct OnEcsMonoBehaviorDestroyedSignal
    {
        public IEcsMonoBehavior EcsMonoBehavior;
    }

    #endregion
}