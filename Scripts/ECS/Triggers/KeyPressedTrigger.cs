using OneLab.Scripts.ECS.Core.Interfaces;
using OneLab.Scripts.ECS.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.ECS.Triggers
{
    [AddComponentMenu("1Lab/Triggers/KeyPressed")]
    public class KeyPressedTrigger : EcsComponent
    {
        [SerializeField] public KeyCode key;
        public UnityEvent<int, Componenter> onPressed;
        
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
            ref var keyPressedTriggerData = ref Componenter.AddOrGet<KeyPressedTriggerData>(Entity);
            keyPressedTriggerData.Key = key;
            keyPressedTriggerData.OnPressed = onPressed;
        }

        public void Stop()
        {
            Componenter.Del<KeyPressedTriggerData>(Entity);
        }
    }

    public struct KeyPressedTriggerData : IEcsComponent
    {
        public KeyCode Key;
        public UnityEvent<int, Componenter> OnPressed;
    }
}