
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/SpeedLimiter")]
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpeedLimiterComponent : OneLabComponent
    {
        [SerializeField] private bool hasXLimit;
        [SerializeField] private float xLimit = 5f;
        [SerializeField] private bool hasYLimit;
        [SerializeField] private float yLimit = 5f;

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
            if (hasXLimit)
            {
                ref var speedLimitXData = ref Pooler.SpeedLimitX.AddOrGet(Entity);
                speedLimitXData.Limit = xLimit;
            }            
            if (hasYLimit)
            {
                ref var speedLimitXData = ref Pooler.SpeedLimitY.AddOrGet(Entity);
                speedLimitXData.Limit = yLimit;
            }
        }

        public void Stop()
        {
            Pooler.SpeedLimitX.Del(Entity);
            Pooler.SpeedLimitY.Del(Entity);
        }
    }
}