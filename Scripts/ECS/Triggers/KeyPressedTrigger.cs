
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Triggers
{
    [AddComponentMenu("1Lab/Triggers/KeyPressed")]
    public class KeyPressedTrigger : OneLabComponent
    {
        [SerializeField] public KeyCode key;
        public UnityEvent<int, OneLabPooler> onPressed;
        
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
            ref var keyPressedTriggerData = ref Pooler.KeyPressedTrigger.AddOrGet(Entity);
            keyPressedTriggerData.Key = key;
            keyPressedTriggerData.OnPressed = onPressed;
        }

        public void Stop()
        {
            Pooler.KeyPressedTrigger.Del(Entity);
        }
    }
}