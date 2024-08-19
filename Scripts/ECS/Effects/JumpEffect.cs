using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Effects
{
    [AddComponentMenu("1Lab/Effects/Gravitation")]
    public class JumpEffect : EcsEffect
    {
        private readonly OneLabSignals.CommandTryInvokeJumpSignal _commandTryInvokeJumpSignal = new();
        
        public void TryInvokeJump()
        {
            OneLab.Signal.RegistryRaise(_commandTryInvokeJumpSignal);
        }
    }
}