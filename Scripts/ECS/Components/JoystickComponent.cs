
using Exerussus._1EasyEcs.Scripts.Core;
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
                ref var joystickXData = ref Componenter.AddOrGet<JoystickXData>(Entity);
                joystickXData.Value = joystick;
                joystickXData.FullMagnitude = fullMagnitude;
            }
            if (hasY)
            {
                ref var joystickXData = ref Componenter.AddOrGet<JoystickYData>(Entity);
                joystickXData.Value = joystick;
                joystickXData.FullMagnitude = fullMagnitude;
            }
        }

        public void Stop()
        {
            Componenter.Del<JoystickXData>(Entity);
            Componenter.Del<JoystickYData>(Entity);
        }
        
        protected override void OnValidate()
        {
            base.OnValidate();
            joystick = gameObject.TryGetIfNull(ref joystick);
        }
    }

    public struct JoystickXData : IEcsComponent
    {
        public bool FullMagnitude;
        public Joystick Value;
    }
        
    public struct JoystickYData : IEcsComponent
    {
        public bool FullMagnitude;
        public Joystick Value;
    }
}