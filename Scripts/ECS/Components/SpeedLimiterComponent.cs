
using _1Lab.Scripts.ECS.Core.Interfaces;
using _1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace _1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/SpeedLimiter")]
    [RequireComponent(typeof(PhysicalBodyComponent))]
    public class SpeedLimiterComponent : EcsComponent
    {
        [SerializeField] private bool hasXLimit;
        [SerializeField] private float xLimit = 5f;
        [SerializeField] private bool hasYLimit;
        [SerializeField] private float yLimit = 5f;
        [SerializeField, HideInInspector] private PhysicalBodyComponent physicalBodyComponent;

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
                ref var speedLimitXData = ref Componenter.AddOrGet<SpeedLimitXData>(Entity);
                speedLimitXData.PhysicalBody = physicalBodyComponent;
                speedLimitXData.Limit = xLimit;
            }            
            if (hasYLimit)
            {
                ref var speedLimitXData = ref Componenter.AddOrGet<SpeedLimitYData>(Entity);
                speedLimitXData.PhysicalBody = physicalBodyComponent;
                speedLimitXData.Limit = yLimit;
            }
        }

        public void Stop()
        {
            Componenter.Del<SpeedLimitXData>(Entity);
            Componenter.Del<SpeedLimitYData>(Entity);
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            if (physicalBodyComponent == null) physicalBodyComponent = GetComponent<PhysicalBodyComponent>();
        }
    }

    public struct SpeedLimitXData : IEcsComponent
    {
        public PhysicalBodyComponent PhysicalBody;
        public float Limit;
    }

    public struct SpeedLimitYData : IEcsComponent
    {
        public PhysicalBodyComponent PhysicalBody;
        public float Limit;
    }
}