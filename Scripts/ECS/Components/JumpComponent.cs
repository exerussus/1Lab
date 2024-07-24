using OneLab.Scripts.ECS.Core.Interfaces;
using OneLab.Scripts.ECS.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/Jump")]
    [RequireComponent(typeof(PhysicalBodyComponent))]
    public class JumpComponent : EcsComponent
    {
        [SerializeField] private KeyCode key1;
        [SerializeField] private KeyCode key2;
        [SerializeField] private float power;
        [SerializeField] private Vector2 direction;
        [SerializeField] private float coolDown;
        public UnityEvent<int, Componenter> onJump;
        
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
            ref var jumpData = ref Componenter.AddOrGet<JumpData>(Entity);
            jumpData.Direction = direction;
            jumpData.Power = power;
            jumpData.Key1 = key1;
            jumpData.Key2 = key2;
            jumpData.OnJump = onJump;
            jumpData.CoolDownDelay = coolDown;
        }

        public void Stop()
        {
            Componenter.Del<JumpData>(Entity);
        }
    }

    public struct JumpData : IEcsComponent
    {
        public KeyCode Key1;
        public KeyCode Key2;
        public float Power;
        public Vector2 Direction;
        public UnityEvent<int, Componenter> OnJump;
        public float CoolDownDelay;
        public float CoolDownTimer;
    }
}