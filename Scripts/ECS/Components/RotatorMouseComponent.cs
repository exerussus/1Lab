
using _1Lab.Scripts.ECS.Core.Interfaces;
using _1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace _1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/RotatorMouse")]
    public class RotatorMouseComponent : EcsComponent
    {
        [SerializeField] private bool autoRun;
        [SerializeField] private float speed;
        private bool _isActivated;
        
        public override void Initialize()
        {
            if (autoRun) Run();
        }

        public void Run()
        {
            _isActivated = true;
            ref var rotatorMouseData = ref Componenter.AddOrGet<RotatorMouseData>(Entity);
            rotatorMouseData.Speed = speed;
        }

        public void Stop()
        {
            _isActivated = false;
            Componenter.Del<RotatorMouseData>(Entity);
        }

        public void Switch()
        {
            if (_isActivated) Stop();
            else Run();
        }
    }

    public struct RotatorMouseData : IEcsComponent
    {
        public float Speed;
    }
}