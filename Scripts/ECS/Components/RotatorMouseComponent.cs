
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/RotatorMouse")]
    public class RotatorMouseComponent : OneLabComponent
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
            ref var rotatorMouseData = ref Pooler.RotatorMouse.AddOrGet(Entity);
            rotatorMouseData.Speed = speed;
        }

        public void Stop()
        {
            _isActivated = false;
            Pooler.RotatorMouse.Del(Entity);
        }

        public void Switch() 
        {
            if (_isActivated) Stop();
            else Run();
        }
    }
}