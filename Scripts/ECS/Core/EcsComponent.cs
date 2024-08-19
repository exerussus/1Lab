
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.SignalSystem;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.Core
{
    [RequireComponent(typeof(IEcsMonoBehavior))]
    public abstract class EcsComponent : MonoSignalListener
    {
        private int _entity;
        private Componenter _componenter;
        private OneLabPooler _pooler;

        public int Entity => _entity;
        public Componenter Componenter => _componenter;
        public OneLabPooler Pooler => _pooler;

        public void PreInitialize(int entity, Componenter componenter, OneLabPooler pooler)
        {
            _entity = entity;
            _componenter = componenter;
            _pooler = pooler;
        }
        
        public virtual void Initialize()
        {
            
        }

        public virtual void Destroy()
        {
            
        }

        protected override void OnValidate()
        {
            base.OnValidate();
        }
    }
}