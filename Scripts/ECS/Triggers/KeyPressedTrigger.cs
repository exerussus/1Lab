using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Triggers
{
    [AddComponentMenu("1Lab/Triggers/KeyPressed")]
    public class KeyPressedTrigger : OneLabComponent
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

    public struct KeyPressedTriggerData : IOneLabEcsData
    {
        public KeyCode Key;
        public UnityEvent<int, Componenter> OnPressed;
    }
}