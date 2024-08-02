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
            _jumpFilter = Componenter.Filter<JumpData>().Inc<PhysicalBodyData>().End();
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
                ref var physicalBodyData = ref Componenter.Get<PhysicalBodyData>(entity);
                var velocity = physicalBodyData.Rigidbody2D.velocity;
                
                if (velocity.x != 0) velocity.x = 0;
                if (velocity.y != 0) velocity.y = 0;
                physicalBodyData.Rigidbody2D.velocity = velocity;
                
                physicalBodyData.Rigidbody2D.velocity += jumpData.Power * jumpData.Direction;
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
                    ref var physicalBodyData = ref Componenter.Get<PhysicalBodyData>(entity);
                    var resultVelocity = physicalBodyData.Rigidbody2D.velocity;
                    resultVelocity.x = 0;
                    physicalBodyData.Rigidbody2D.velocity = resultVelocity;
                }
                return;
            }

            if (keyboardInputMoverData.UsePhysicalBody)
            {
                ref var physicalBodyData = ref Componenter.Get<PhysicalBodyData>(entity);
                var currentVelocity = physicalBodyData.Rigidbody2D.velocity;
                
                if (keyboardInputMoverData.FullSpeed)
                {
                    currentVelocity.x = keyboardInputMoverData.Speed * inputX;
                    physicalBodyData.Rigidbody2D.velocity = currentVelocity;

                    if (Mathf.Abs(currentVelocity.x) > keyboardInputMoverData.Speed)
                    {
                        currentVelocity.x = Mathf.Sign(currentVelocity.x) * keyboardInputMoverData.Speed;
                        physicalBodyData.Rigidbody2D.velocity = currentVelocity;
                    }
                }
                else
                {
                    currentVelocity.x += keyboardInputMoverData.Speed * Time.deltaTime * inputX;
                    physicalBodyData.Rigidbody2D.velocity = currentVelocity;
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