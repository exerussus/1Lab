
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/Jump")]
    [RequireComponent(typeof(Rigidbody2D))]
    public class JumpComponent : OneLabComponent
    {
        [SerializeField] private KeyCode key1;
        [SerializeField] private KeyCode key2;
        [SerializeField] private float power;
        [SerializeField] private Vector2 direction;
        [SerializeField] private float coolDown;
        [SerializeField] private bool reloadOnTouch;
        [SerializeField] private string[] touchTags;
        public UnityEvent<int, Componenter, OneLabPooler> onJump;
        private const float MaxReloadOnTouch = 0.2f;
        
        public override void Initialize()
        {
            Run();
        }

        public override void Destroy()
        {
            Stop();
        }

        public void Run()
        {
            ref var jumpData = ref Pooler.Jump.AddOrGet(Entity);
            jumpData.Direction = direction;
            jumpData.Power = power;
            jumpData.Key1 = key1;
            jumpData.Key2 = key2;
            jumpData.OnJump = onJump;
            jumpData.CoolDownDelay = coolDown;
            jumpData.ReloadOnTouch = reloadOnTouch;
            jumpData.TouchTags = touchTags;
        }

        public void Stop()
        {
            Pooler.Jump.Del(Entity);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            TryReload(other);
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            TryReload(other.collider);
        }

        private void TryReload(Collider2D other)
        {
            var isExist = false;
            foreach (var touchTag in touchTags)
            {
                if (other.CompareTag(touchTag))
                {
                    isExist = true;
                    break;
                }
            }
            
            if (isExist)
            {
                if (Pooler.Jump.Has(Entity))
                {
                    ref var jumpData = ref Pooler.Jump.Get(Entity);
                    if (jumpData is { ReloadOnTouch: true, CoolDownTimer: > MaxReloadOnTouch }) jumpData.CoolDownTimer = MaxReloadOnTouch;
                }
            }
        }
    }
}