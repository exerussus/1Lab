using OneLab.Scripts.ECS.Core;
using Source.Scripts.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Source.Scripts.ECS.Systems
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
            if ((Input.GetKeyDown(jumpData.Key1) || Input.GetKeyDown(jumpData.Key2)) && jumpData.CoolDownTimer < 0)
            {
                ref var physicalBodyData = ref Componenter.Get<PhysicalBodyData>(entity);
                physicalBodyData.Rigidbody2D.velocity += jumpData.Power * jumpData.Direction;
                jumpData.OnJump?.Invoke(entity, Componenter);
                jumpData.CoolDownTimer = jumpData.CoolDownDelay;
            }
        }

        private void OnKeyboardInputMoverUpdate(int entity)
        {
            var input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            ref var keyboardInputMoverData = ref Componenter.Get<KeyboardPlatformInputMoverData>(entity);

            if (input == Vector2.zero)
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

            var result = input.normalized;

            if (keyboardInputMoverData.UsePhysicalBody)
            {
                ref var physicalBodyData = ref Componenter.Get<PhysicalBodyData>(entity);
                var currentVelocity = physicalBodyData.Rigidbody2D.velocity;
                
                if (keyboardInputMoverData.FullSpeed)
                {
                    currentVelocity.x = keyboardInputMoverData.Speed * result.x;
                    physicalBodyData.Rigidbody2D.velocity = currentVelocity;

                    if (Mathf.Abs(currentVelocity.x) > keyboardInputMoverData.Speed)
                    {
                        currentVelocity.x = Mathf.Sign(currentVelocity.x) * keyboardInputMoverData.Speed;
                        physicalBodyData.Rigidbody2D.velocity = currentVelocity;
                    }
                }
                else
                {
                    currentVelocity.x += keyboardInputMoverData.Speed * Time.deltaTime * result.x;
                    physicalBodyData.Rigidbody2D.velocity = currentVelocity;
                }
            }
            else
            {
                ref var transformData = ref Componenter.Get<TransformData>(entity);
                transformData.Value.Translate(keyboardInputMoverData.Speed * Time.deltaTime * result);
            }
        }
    }
}