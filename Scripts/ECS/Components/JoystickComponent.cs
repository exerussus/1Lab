
using Exerussus._1Extensions.Scripts.Extensions;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;


namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/Joystick")]
    [RequireComponent(typeof(Joystick))]
    public class JoystickComponent : OneLabComponent
    {
        public bool autoRun = true;
        public bool hasX;
        public bool hasY;
        public bool fullMagnitude = true;
        [HideInInspector] public Joystick joystick;

        public override void Initialize()
        {
            if (autoRun) Run();
        }

        public override void Destroy()
        {
            Stop();
        }

        public void Run()
        {
            if (hasX)
            {
                ref var joystickXData = ref Componenter.AddOrGet<OneLabData.JoystickXData>(Entity);
                joystickXData.Value = joystick;
                joystickXData.FullMagnitude = fullMagnitude;
            }
            if (hasY)
            {
                ref var joystickXData = ref Componenter.AddOrGet<OneLabData.JoystickYData>(Entity);
                joystickXData.Value = joystick;
                joystickXData.FullMagnitude = fullMagnitude;
            }
        }

        public void Stop()
        {
            Componenter.Del<OneLabData.JoystickXData>(Entity);
            Componenter.Del<OneLabData.JoystickYData>(Entity);
        }
        
        protected override void OnValidate()
        {
            base.OnValidate();
            joystick = gameObject.TryGetIfNull(ref joystick);
        }
    }
}