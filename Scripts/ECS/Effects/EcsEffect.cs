
using Source.Scripts.SignalSystem;
using UnityEngine;

namespace Source.Scripts.ECS.Effects
{
    public class EcsEffect : MonoSignalListener
    {
        [SerializeField] private bool activated = true;
        public bool Activated => activated;

        public void SetActivated(bool isActivated)
        {
            activated = isActivated;
        }
        
        public void Activate()
        {
            activated = true;
        }
        
        public void Deactivate()
        {
            activated = false;
        }
    }
}