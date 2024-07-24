using OneLab.Scripts.ECS.Core.Interfaces;
using UnityEngine;

namespace OneLab.Scripts.ECS.Core
{
    public struct TransformData : IEcsData<Transform>
    {
        public Transform Value;
        public void InitializeValues(Transform value)
        {
            Value = value;
        }
    }
    public struct OnDestroyData : IEcsData<GameObject, float>
    {
        public float TimeRemaining;
        public GameObject ObjectToDelete;
        public void InitializeValues(GameObject objectToDelete, float value)
        {
            TimeRemaining = value;
            ObjectToDelete = objectToDelete;
        }
    }
}