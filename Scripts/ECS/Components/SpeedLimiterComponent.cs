
using Exerussus._1EasyEcs.Scripts.Core;
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
                ref var speedLimitXData = ref Componenter.AddOrGet<OneLabData.SpeedLimitXData>(Entity);
                speedLimitXData.Limit = xLimit;
            }            
            if (hasYLimit)
            {
                ref var speedLimitXData = ref Componenter.AddOrGet<OneLabData.SpeedLimitYData>(Entity);
                speedLimitXData.Limit = yLimit;
            }
        }

        public void Stop()
        {
            Componenter.Del<OneLabData.SpeedLimitXData>(Entity);
            Componenter.Del<OneLabData.SpeedLimitYData>(Entity);
        }
    }
}