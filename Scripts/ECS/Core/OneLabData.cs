
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Components;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public static class OneLabData
    {
        
        public struct JoystickY : IOneLabEcsData
        {
            public bool FullMagnitude;
            public Joystick Value;
        }

        public struct JoystickX : IOneLabEcsData
        {
            public bool FullMagnitude;
            public Joystick Value;
        }
        
        public struct Transform : IOneLabEcsData
        {
            public UnityEngine.Transform Value;
            public void InitializeValues(UnityEngine.Transform value)
            {
                Value = value;
            }
        }

        public struct RigidBody2D : IOneLabEcsData
        {
            public Rigidbody2D Value;
        }

        public struct RigidBody3D : IOneLabEcsData
        {
            public Rigidbody Value;
        }
    
        public struct OnDestroy : IOneLabEcsData
        {
            public float TimeRemaining;
            public GameObject ObjectToDelete;
            public void InitializeValues(GameObject objectToDelete, float value)
            {
                TimeRemaining = value;
                ObjectToDelete = objectToDelete;
            }
        }

        public struct AlphaColorProcess : IOneLabEcsData
        {
            public AlphaColorComponent AlphaColor;
            public float Speed;
            public UnityEvent<int, Componenter, OneLabPooler> OnSuccess;
        }
        
        public struct CharacterAnimatorExpended : IOneLabEcsData
        {
            public bool IsOneShot;
            public int FrameRemaining;
            public CharacterAnimatorComponent.SpritePack CurrentPack;
            public int CurrentSprite;
            public CharacterAnimatorComponent Value;
        }

        public struct CompositeObject : IOneLabEcsData
        {
            public CompositeObjectComponent Value;
        }

        public struct Jump : IOneLabEcsData
        {
            public KeyCode Key1;
            public KeyCode Key2;
            public float Power;
            public Vector2 Direction;
            public UnityEvent<int, Componenter, OneLabPooler> OnJump;
            public float CoolDownDelay;
            public float CoolDownTimer;
            public bool ReloadOnTouch;
            public string[] TouchTags;
        }

        public struct KeyColliderSwitcher : IOneLabEcsData
        {
            public KeyColliderSwitcherComponent Value;
        }

        public struct KeyboardPlatformInputMover : IOneLabEcsData
        {
            public float Speed;
            public bool FullSpeed;
            public bool UsePhysicalBody;
            public bool StopXWithoutInput;
            public Collider2D CharacterCollider2D;
            public bool HasXJoystick;
            public bool HasYJoystick;
        }

        public struct PointMover : IOneLabEcsData
        {
            public float Speed;
            public Vector2 StartPoint;
            public Vector2 EndPoint;
            public bool ToEndPoint;
        }

        public struct PointRotator : IOneLabEcsData
        {
            public Vector2 Point;
            public float Speed;
        }

        public struct RotatorMouse : IOneLabEcsData
        {
            public float Speed;
        }

        public struct SelfRotatorX : IOneLabEcsData
        {
            public float Speed;
        }

        public struct SelfRotatorY : IOneLabEcsData
        {
            public float Speed;
        }

        public struct SelfRotatorZ : IOneLabEcsData
        {
            public float Speed;
        }

        public struct SmartCamera : IOneLabEcsData
        {
            public UnityEngine.Transform Transform;
            public float SmoothingSpeed;
            public Vector3 Velocity;
            public float SmoothingTime;
        }

        public struct Sorter : IOneLabEcsData
        {
            public SortRendererComponent Value;
        }

        public struct SpeedLimitX : IOneLabEcsData
        {
            public float Limit;
        }

        public struct SpeedLimitY : IOneLabEcsData 
        {
            public float Limit;
        }

        public struct Touchable : IOneLabEcsData
        {
            public TouchableComponent Value;
        }

        public struct CharacterAnimator : IOneLabEcsData
        {
            public bool IsOneShot;
            public int FrameRemaining;
            public UniversalAnimatorComponent.SpritePack CurrentPack;
            public int CurrentSprite;
            public UniversalAnimatorComponent Value;
        }

        public struct Vfx : IOneLabEcsData
        {
            public VfxComponent Value;
            public int FramesRemaining;
            public float LoopTimeRemaining;
        }

        public struct RequestReleaseVfxMark : IOneLabEcsData
        {
        
        }

        public struct Visual : IOneLabEcsData
        {
            public SpriteRenderer SpriteRenderer;
        }

        public struct Tags : IOneLabEcsData
        {
            public string[] Values;
        }
    
        public struct CameraTarget : IOneLabEcsData
        {
            public bool FollowX;
            public bool FollowY;
            public Vector3 Offset;
            public UnityEngine.Transform Transform;
        }

        public struct AnimationInput : IOneLabEcsData
        {
            public float HorizontalAxis;
        }

        public struct Input : IOneLabEcsData
        {
            public float Horizontal;
            public float Vertical;
        }

        public struct EntityDeathTrigger : IOneLabEcsData
        {
            public UnityEvent<int, Componenter, OneLabPooler> OnDead;
        }

        public struct GlobalTrigger : IOneLabEcsData
        {
            public string[] Tags;
            public Triggers.GlobalTrigger Value;
        }

        public struct KeyPressedTrigger : IOneLabEcsData
        {
            public KeyCode Key;
            public UnityEvent<int, Componenter, OneLabPooler> OnPressed;
        }

        public struct TimerTrigger : IOneLabEcsData
        {
            public UnityEvent<int, Componenter, OneLabPooler> OnTick;
            public float Delay;
            public float Timer;
            public bool IsLoop;
        }

        public struct Fliper : IOneLabEcsData
        {
            public FliperComponent Value;
        }

        public struct EcsMonoBehavior : IOneLabEcsData
        {
            public IEcsMonoBehavior Value;
        
            public void InitializeValues(IEcsMonoBehavior value)
            {
                Value = value;
            }
        }

        public struct DirectionMover : IOneLabEcsData
        {
            public float Speed;
            public Vector2 Direction;
        }
    }
}