
using System;
using OneLab.Scripts.ECS.Core.Interfaces;
using Source.Scripts.Data.GamesConfigurations;
using OneLab.Scripts.ECS.Core;
using Source.Scripts.Managers.ProjectSettings;
using Source.Scripts.SignalSystem;
using UnityEngine;

namespace OneLab.Scripts.ECS.Core
{
    public sealed class EcsMonoBehavior : MonoBehaviour
    {
        #region SerializedFields
        
        [SerializeField] private int entity;
        [SerializeField] private bool isAlive = true;
        [SerializeField] private bool isInitialized;
        [SerializeField] private int components;
        [SerializeField, HideInInspector] private EcsComponent[] ecsComponents;
        [SerializeField, HideInInspector] private OneLabConfiguration oneLabConfiguration;
        private bool _isReused;
        
        #endregion

        #region Members
        
        public int Entity => entity;
        public bool IsAlive => isAlive;
        public Componenter Componenter { get; private set; }
        public event Action onInitialized;

        #endregion

        #region InitAndDestroy
        
        private void Start()
        {
            Initialize();
        }

        private void OnEnable()
        {
            if (_isReused)
            {
                if (isInitialized) return;
                isInitialized = true;
                Initialize();
            }
        }

        private void OnDisable()
        {
            if (isInitialized) foreach (var ecsComponent in ecsComponents) ecsComponent.Destroy();
        }

        private void Initialize()
        {
            isAlive = true;
            Componenter = OneLab.Componenter;
            entity = Componenter.GetNewEntity();
            ref var transformData = ref Componenter.Add<TransformData>(entity);
            transformData.InitializeValues(transform);
            foreach (var ecsComponent in ecsComponents) ecsComponent.PreInitialize(entity, Componenter);
            foreach (var ecsComponent in ecsComponents) ecsComponent.Initialize();
            ref var ecsMonoBehData = ref Componenter.Add<EcsMonoBehaviorData>(entity);
            ecsMonoBehData.InitializeValues(this);
            oneLabConfiguration.Signal.RegistryRaise(new OnEcsMonoBehaviorInitializedSignal { EcsMonoBehavior = this });
            onInitialized?.Invoke();
            onInitialized = null;
        }
        
        public void DestroyEcsMonoBehavior(float delay)
        {
            if (!IsAlive) return;
            isAlive = false;
            isInitialized = false;
            _isReused = true;
            foreach (var ecsComponent in ecsComponents) ecsComponent.Destroy();
            oneLabConfiguration.Signal.RegistryRaise(new OnEcsMonoBehaviorStartDestroySignal { EcsMonoBehavior = this });
            ref var destroyingData = ref Componenter.AddOrGet<OnDestroyData>(entity);
            destroyingData.InitializeValues(gameObject, delay);
        }

        #endregion

        #region Editor

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (oneLabConfiguration == null) oneLabConfiguration = Project.Loader.GetAssetByTypeOnValidate<OneLabConfiguration>();
            ecsComponents = GetComponents<EcsComponent>();
            components = ecsComponents.Length;
        }
#endif
        
        #endregion
    }

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

    public struct EcsMonoBehaviorData : IEcsData<EcsMonoBehavior>
    {
        public EcsMonoBehavior Value;
        
        public void InitializeValues(EcsMonoBehavior value)
        {
            Value = value;
        }
    }
}