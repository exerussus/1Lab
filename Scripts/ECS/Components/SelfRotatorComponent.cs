
using System;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/SelfRotator")]
    public class SelfRotatorComponent : OneLabComponent
    {
        [SerializeField] private RotationSettings x;
        [SerializeField] private RotationSettings y;
        [SerializeField] private RotationSettings z;

        public override void Initialize()
        {
            Run();
        }

        public override void Destroy()
        {
            Stop();
        }

        public void SetSpeedX(float value)
        {
            x.speed = value;
            Run();
        }
        
        public void AddSpeedX(float value)
        {
            x.speed += value;
            Run();
        }

        public void ReverseX()
        {
            x.reversed = !x.reversed;
            Run();
        }

        public void SetSpeedY(float value)
        {
            y.speed = value;
            Run();
        }
        
        public void AddSpeedY(float value)
        {
            y.speed += value;
            Run();
        }

        public void ReverseY()
        {
            y.reversed = !y.reversed;
            Run();
        }

        public void SetSpeedZ(float value)
        {
            z.speed = value;
            Run();
        }
        
        public void AddSpeedZ(float value)
        {
            z.speed += value;
            Run();
        }

        public void ReverseZ()
        {
            z.reversed = !z.reversed;
            Run();
        }
        
        public void Run()
        {
            if (x.enabled)
            {
                ref var selfRotatorData = ref Pooler.SelfRotatorX.AddOrGet(Entity);
                selfRotatorData.Speed = x.reversed? -x.speed : x.speed;
            }
            if (y.enabled)
            {
                ref var selfRotatorData = ref Pooler.SelfRotatorY.AddOrGet(Entity);
                selfRotatorData.Speed = y.reversed? -y.speed : y.speed;
            }
            if (z.enabled)
            {
                ref var selfRotatorData = ref Pooler.SelfRotatorZ.AddOrGet(Entity);
                selfRotatorData.Speed = z.reversed? -z.speed : z.speed;
            }
        }

        public void Stop()
        {
            Pooler.SelfRotatorX.Del(Entity);
            Pooler.SelfRotatorY.Del(Entity);
            Pooler.SelfRotatorZ.Del(Entity);
        }
        
        [Serializable]
        public class RotationSettings
        {
            [SerializeField] public bool enabled;
            [SerializeField] public float speed = 25;
            [SerializeField] public bool reversed;
        }
    }
}