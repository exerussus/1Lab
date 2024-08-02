﻿
using Exerussus._1EasyEcs.Scripts.Core;
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
            ref var keyboardInputMoverData = ref Componenter.AddOrGet<KeyboardPlatformInputMoverData>(Entity);
            keyboardInputMoverData.Speed = speed;
            keyboardInputMoverData.UsePhysicalBody = usePhysicalBody;
            keyboardInputMoverData.FullSpeed = fullSpeed;
            keyboardInputMoverData.StopXWithoutInput = stopXWithoutInput;
        }

        public void Stop()
        {
            Componenter.Del<KeyboardPlatformInputMoverData>(Entity);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (usePhysicalBody)
            {
                var physicalBody = GetComponent<PhysicalBodyComponent>();
                if (physicalBody == null) gameObject.AddComponent<PhysicalBodyComponent>();
            }
        }
    }

    public struct KeyboardPlatformInputMoverData : IEcsComponent
    {
        public float Speed;
        public bool FullSpeed;
        public bool UsePhysicalBody;
        public bool StopXWithoutInput;
        public Collider2D CharacterCollider2D;
    }
}