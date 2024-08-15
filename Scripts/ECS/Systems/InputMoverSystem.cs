using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;
using Exerussus._1Lab.Scripts.ECS.Components;
using Exerussus._1Lab.Scripts.ECS.Core;
using Exerussus._1Lab.Scripts.ECS.Effects;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class InputMoverSystem : EcsSignalListener<IOneLabEcsData, CommandTryInvokeJumpSignal>
    {
        private EcsFilter _keyboardInputMoverFilter;
        private EcsFilter _jumpFilter;
        private EcsFilter _joystickXFilter;
        private EcsFilter _joystickYFilter;
        private Pooler _pooler;

        protected override void Initialize()
        {
            _keyboardInputMoverFilter = Componenter.Filter<KeyboardPlatformInputMoverData>().End();
            _jumpFilter = Componenter.Filter<JumpData>().Inc<RigidBody2DData>().End();
            _joystickXFilter = Componenter.Filter<JoystickXData>().End();
            _joystickYFilter = Componenter.Filter<JoystickYData>().End();
            _pooler = GameShare.GetSharedObject<Pooler>();
        }

        protected override void Update()
        {
            _joystickXFilter.Foreach(UpdateXInput);
            _joystickYFilter.Foreach(UpdateYInput);
            _keyboardInputMoverFilter.Foreach(OnKeyboardInputMoverUpdate);
            _jumpFilter.Foreach(OnJumpUpdate);
        }

        protected override void OnSignal(CommandTryInvokeJumpSignal data)
        {
            foreach (var entity in _jumpFilter)
            {
                ref var jumpData = ref _pooler.Jump.Get(entity);
                if (jumpData.CoolDownTimer < 0)
                {
                    ref var physicalBodyData = ref _pooler.RigidBody2D.Get(entity);
                    var velocity = physicalBodyData.Value.velocity;
                
                    if (velocity.x != 0) velocity.x = 0;
                    if (velocity.y != 0) velocity.y = 0;
                    physicalBodyData.Value.velocity = velocity;
                
                    physicalBodyData.Value.velocity += jumpData.Power * jumpData.Direction;
                    jumpData.OnJump?.Invoke(entity, Componenter);
                    jumpData.CoolDownTimer = jumpData.CoolDownDelay;
                }
            }
        }

        private void UpdateYInput(int joystickEntity)
        {
            ref var joystickData = ref _pooler.JoystickY.Get(joystickEntity);
            foreach (var entity in _keyboardInputMoverFilter)
            {
                ref var keyboardData = ref _pooler.KeyboardPlatformInputMover.Get(entity);
                if (!keyboardData.HasYJoystick) continue;
                
                ref var inputData = ref _pooler.Input.AddOrGet(entity);
                inputData.Vertical = joystickData.Value.Vertical;
                
                if (joystickData.FullMagnitude)
                {
                    if (inputData.Vertical > 0) inputData.Vertical = 1;
                    else if (inputData.Vertical < 0) inputData.Vertical = -1;
                }
            }
        }

        private void UpdateXInput(int joystickEntity)
        {
            ref var joystickData = ref _pooler.JoystickX.Get(joystickEntity);
            foreach (var entity in _keyboardInputMoverFilter)
            {
                ref var keyboardData = ref _pooler.KeyboardPlatformInputMover.Get(entity);
                if (!keyboardData.HasXJoystick) continue;
                
                ref var inputData = ref _pooler.Input.AddOrGet(entity);
                inputData.Horizontal = joystickData.Value.Horizontal;

                if (joystickData.FullMagnitude)
                {
                    if (inputData.Horizontal > 0) inputData.Horizontal = 1;
                    else if (inputData.Horizontal < 0) inputData.Horizontal = -1;
                }
            }
        }

        private bool InputYCondition(int entity)
        {
            return _pooler.Input.Has(entity) && _pooler.Input.Get(entity).Vertical > 0.3f;
        }
        
        private void OnJumpUpdate(int entity)
        {
            ref var jumpData = ref _pooler.Jump.Get(entity);
            jumpData.CoolDownTimer -= Time.deltaTime;
            
            if ((InputYCondition(entity) || Input.GetKey(jumpData.Key1) || Input.GetKey(jumpData.Key2)) && jumpData.CoolDownTimer < 0)
            {
                ref var physicalBodyData = ref _pooler.RigidBody2D.Get(entity);
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
            ref var keyboardInputMoverData = ref _pooler.KeyboardPlatformInputMover.Get(entity);
            float inputX;
            if (keyboardInputMoverData.HasXJoystick && _pooler.Input.Has(entity)) inputX = _pooler.Input.Get(entity).Horizontal;
            else inputX = Input.GetAxis("Horizontal");

            if (_pooler.AnimationInput.Has(entity))
            {
                ref var animationInputData = ref _pooler.AnimationInput.Get(entity);
                animationInputData.HorizontalAxis = inputX;
            }
            
            if (inputX == 0)
            {
                if (keyboardInputMoverData.StopXWithoutInput && keyboardInputMoverData.UsePhysicalBody)
                {
                    ref var physicalBodyData = ref _pooler.RigidBody2D.Get(entity);
                    var resultVelocity = physicalBodyData.Value.velocity;
                    resultVelocity.x = 0;
                    physicalBodyData.Value.velocity = resultVelocity;
                }
                return;
            }

            if (keyboardInputMoverData.UsePhysicalBody)
            {
                ref var physicalBodyData = ref _pooler.RigidBody2D.Get(entity);
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
                ref var transformData = ref _pooler.Transform.Get(entity);
                transformData.Value.Translate(keyboardInputMoverData.Speed * Time.deltaTime * new Vector2(inputX, 0));
            }
        }
    }

    public struct InputData : IOneLabEcsData
    {
        public float Horizontal;
        public float Vertical;
    }
}