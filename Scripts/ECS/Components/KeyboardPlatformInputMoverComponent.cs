
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/KeyboardPlatformInputMover")]
    public class KeyboardPlatformInputMoverComponent : OneLabComponent
    {
        [SerializeField] private float speed = 5;
        [SerializeField] private bool fullSpeed;
        [SerializeField] private bool usePhysicalBody;
        [SerializeField] private bool stopXWithoutInput;
        [SerializeField] private bool hasXJoystick;
        [SerializeField] private bool hasYJoystick;
        
        public override void Initialize()
        {
            Run();
        }

        public override void Destroy()
        {
            Stop();
        }

        public void SetSpeed(float value)
        {
            speed = value;
            Run();
        }

        public void Run()
        {
            ref var keyboardInputMoverData = ref Pooler.KeyboardPlatformInputMover.AddOrGet(Entity);
            keyboardInputMoverData.Speed = speed;
            keyboardInputMoverData.UsePhysicalBody = usePhysicalBody;
            keyboardInputMoverData.FullSpeed = fullSpeed;
            keyboardInputMoverData.StopXWithoutInput = stopXWithoutInput;
            keyboardInputMoverData.HasXJoystick = hasXJoystick;
            keyboardInputMoverData.HasYJoystick = hasYJoystick;
        }

        public void Stop()
        {
            Pooler.KeyboardPlatformInputMover.Del(Entity);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (usePhysicalBody)
            {
                var physicalBody = GetComponent<Rigidbody2D>();
                if (physicalBody == null) gameObject.AddComponent<Rigidbody2D>();
            }
        }
    }
}