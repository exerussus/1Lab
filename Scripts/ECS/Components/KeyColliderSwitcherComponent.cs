
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/KeyColliderSwitcher")]
    public class KeyColliderSwitcherComponent : OneLabComponent
    {
        public bool autoRun = true;
        public KeyCode key1 = KeyCode.S;
        public KeyCode key2 = KeyCode.DownArrow;
        public Collider2D _collider2D;
        public string playerTag = "Player";
        public bool isPlayerExist;
        public bool deactivateOnExist = true;

        public override void Initialize()
        {
            if (autoRun) Run();
        }

        public void Run()
        {
            ref var keyColliderSwitcherData = ref Componenter.AddOrGet<KeyColliderSwitcherData>(Entity);
            keyColliderSwitcherData.Value = this;
        }

        public void Stop()
        {
            Componenter.Del<KeyColliderSwitcherData>(Entity);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(playerTag)) isPlayerExist = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(playerTag)) isPlayerExist = false;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag(playerTag)) isPlayerExist = true;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.CompareTag(playerTag)) isPlayerExist = false;
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (_collider2D == null) _collider2D = GetComponent<Collider2D>();
        }
    }

    public struct KeyColliderSwitcherData : IEcsComponent
    {
        public KeyColliderSwitcherComponent Value;
    }
}