using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.Scripts.Extensions;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/Touchable")]
    [RequireComponent(typeof(Collider2D))]
    public class TouchableComponent : OneLabComponent
    {
        [SerializeField, HideInInspector] public Collider2D touchableCollider2D;
        [SerializeField] public string[] targetTags;
        [SerializeField] public bool singleUse;
        public UnityEvent<int, int, Componenter> onTouch;
        public UnityEvent<int, int, Componenter> onExit;
        public bool IsInitialized { get; private set; } = false;
        
        public string[] TargetTags => targetTags;
        private bool _isUsed;

        public override void Initialize()
        {
            IsInitialized = true;
            ref var touchableData = ref Componenter.AddOrGet<OneLabData.TouchableData>(Entity);
            touchableData.Value = this;
        }

        public override void Destroy()
        {
            IsInitialized = false;
            _isUsed = false;
            Componenter.Del<OneLabData.TouchableData>(Entity);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!IsInitialized) return;
            if (singleUse && _isUsed) return;
            if (other.TryGetComponent(out TouchableComponent touchable))
            {
                if (!touchable.IsInitialized) return;

                if (!targetTags.ContainsAny(touchable.OneLabEntity.tags)) return;
                onTouch?.Invoke(Entity, touchable.Entity, Componenter);
                if (singleUse) _isUsed = true;
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!IsInitialized) return;
            if (singleUse && _isUsed) return;
            if (other.collider.TryGetComponent(out TouchableComponent touchable))
            {
                if (!touchable.IsInitialized) return;

                if (!targetTags.ContainsAny(touchable.OneLabEntity.tags)) return;
                onTouch?.Invoke(Entity, touchable.Entity, Componenter);
                if (singleUse) _isUsed = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!IsInitialized) return;
            if (singleUse && _isUsed) return;
            if (other.TryGetComponent(out TouchableComponent touchable))
            {
                if (!touchable.IsInitialized) return;
                if (!targetTags.ContainsAny(touchable.OneLabEntity.tags)) return;
                onExit?.Invoke(Entity, touchable.Entity, Componenter);
                if (singleUse) _isUsed = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (!IsInitialized) return;
            if (singleUse && _isUsed) return;
            if (other.collider.TryGetComponent(out TouchableComponent touchable))
            {
                if (!touchable.IsInitialized) return;
                if (!targetTags.ContainsAny(touchable.OneLabEntity.tags)) return;
                onExit?.Invoke(Entity, touchable.Entity, Componenter);
                if (singleUse) _isUsed = true;
            }
        }

        public void SwitchColliderActivation()
        {
            touchableCollider2D.enabled = !touchableCollider2D.enabled;
        }
        
        protected override void OnValidate()
        {
            base.OnValidate();
            touchableCollider2D = gameObject.TryGetIfNull(ref touchableCollider2D);
        }
    }
}