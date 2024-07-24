using OneLab.Scripts.ECS.Core;
using Source.Scripts.SignalSystem;
using UnityEngine;

namespace OneLab.Scripts.ECS.Core
{
    [RequireComponent(typeof(EcsMonoBehavior))]
    public abstract class EcsComponent : MonoSignalListener, IEcsComponentInitialize, IEcsComponentDestroy
    {
        private int _entity;
        private Componenter _componenter;

        public int Entity => _entity;
        public Componenter Componenter => _componenter;
        
        public void PreInitialize(int entity, Componenter componenter)
        {
            _entity = entity;
            _componenter = componenter;
        }
        
        public virtual void Initialize()
        {
            
        }

        public virtual void Destroy()
        {
            
        }
    }
}