using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Exerussus._1Lab.Scripts.ECS.Components;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class InputMoverSystem : EasySystem
    {
        private EcsFilter _keyboardInputMoverFilter;
        private EcsFilter _jumpFilter;
        
        protected override void Initialize()
        {
            _keyboardInputMoverFilter = Componenter.Filter<KeyboardPlatformInputMoverData>().End();
            _jumpFilter = Componenter.Filter<JumpData>().Inc<RigidBody2DData>().End();
        }

        protected override void Update()
        {
            _keyboardInputMoverFilter.Foreach(OnKeyboardInputMoverUpdate);
            _jumpFilter.Foreach(OnJumpUpdate);
        }

        private void OnJumpUpdate(int entity)
        {
            ref var jumpData = ref Componenter.Get<JumpData>(entity);
            jumpData.CoolDownTimer -= Time.deltaTime;
            
            if ((Input.GetKey(jumpData.Key1) || Input.GetKey(jumpData.Key2)) && jumpData.CoolDownTimer < 0)
            {
                ref var physicalBodyData = ref Componenter.Get<RigidBody2DData>(entity);
                var velocity = physicalBodyData.Value.velocity;
                
                if (velocity.x != 0) velocity.x = 0;
                if (velocity.y != 0) velocity.y = 0;
                physicalBodyData.Value.velocity = velocity;
                
                physicalBodyData.Value.velocity += jumpData.Power * jumpData.Direction;
                jumpData.OnJump?.Invoke(entity, Componenter);
                jumpData.CoolDownTimer = jumpData.CoolDownDelay;
            }
        }

        private void OnKeyboardInputMoverUpdate(int entity)
        {
            var inputX = Input.GetAxis("Horizontal");
            ref var keyboardInputMoverData = ref Componenter.Get<KeyboardPlatformInputMoverData>(entity);
            
            if (inputX == 0)
            {
                if (keyboardInputMoverData.StopXWithoutInput && keyboardInputMoverData.UsePhysicalBody)
                {
                    ref var physicalBodyData = ref Componenter.Get<RigidBody2DData>(entity);
                    var resultVelocity = physicalBodyData.Value.velocity;
                    resultVelocity.x = 0;
                    physicalBodyData.Value.velocity = resultVelocity;
                }
                return;
            }

            if (keyboardInputMoverData.UsePhysicalBody)
            {
                ref var physicalBodyData = ref Componenter.Get<RigidBody2DData>(entity);
                var currentVelocity = physicalBodyData.Value.velocity;
                
                if (keyboardInputMoverData.FullSpeed)
                {
                    currentVelocity.x = keyboardInputMoverData.Speed * inputX;
                    physicalBodyData.Value.velocity = currentVelocity;

                    if (Mathf.Abs(currentVelocity.x) > keyboardInputMoverData.Speed)
                    {
                        currentVelocity.x = Mathf.Sign(currentVelocity.x) * keyboardInputMoverData.Speed;
                        physicalBodyData.Value.velocity = currentVelocity;
                    }
                }
                else
                {
                    currentVelocity.x += keyboardInputMoverData.Speed * Time.deltaTime * inputX;
                    physicalBodyData.Value.velocity = currentVelocity;
                }
            }
            else
            {
                ref var transformData = ref Componenter.Get<TransformData>(entity);
                transformData.Value.Translate(keyboardInputMoverData.Speed * Time.deltaTime * new Vector2(inputX, 0));
            }
        }
    }
}