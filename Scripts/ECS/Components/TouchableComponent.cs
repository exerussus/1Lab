using _1Lab.Scripts.ECS.Core.Interfaces;
using _1Lab.Scripts.ECS.Core;
using _1Lab.Scripts.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace _1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/Touchable")]
    [RequireComponent(typeof(Collider2D), typeof(TagsComponent))]
    public class TouchableComponent : EcsComponent
    {
        [SerializeField, HideInInspector] public Collider2D touchableCollider2D;
        [SerializeField, HideInInspector] public TagsComponent tags;
        [SerializeField] public string[] targetTags;
        [SerializeField] public bool singleUse;
        private bool _isUsed;
        public UnityEvent<int, int, Componenter> onTouch;
        public UnityEvent<int, int, Componenter> onExit;
        public bool IsInitialized { get; private set; } = false;

        public TagsComponent Tags => tags;
        public string[] TargetTags => targetTags;

        public override void Initialize()
        {
            IsInitialized = true;
            ref var touchableData = ref Componenter.AddOrGet<TouchableData>(Entity);
            touchableData.Value = this;
        }

        public override void Destroy()
        {
            IsInitialized = false;
            _isUsed = false;
            Componenter.Del<TouchableData>(Entity);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!IsInitialized) return;
            if (singleUse && _isUsed) return;
            if (other.TryGetComponent(out TouchableComponent touchable))
            {
                if (!touchable.IsInitialized) return;

                if (!targetTags.ContainsAny(touchable.Tags.Values)) return;
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

                if (!targetTags.ContainsAny(touchable.Tags.Values)) return;
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
                if (!targetTags.ContainsAny(touchable.Tags.Values)) return;
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
                if (!targetTags.ContainsAny(touchable.Tags.Values)) return;
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
            if (tags == null) tags = GetComponent<TagsComponent>();
            if (touchableCollider2D == null) touchableCollider2D = GetComponent<Collider2D>();
        }
    }

    public struct TouchableData : IEcsComponent
    {
        public TouchableComponent Value;
    }
}