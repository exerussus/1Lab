using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Components;
using Exerussus._1Lab.Scripts.ECS.Triggers;
using UnityEngine;
using UnityEngine.Events;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public static class OneLabData
    {
        
        public struct JoystickYData : IOneLabEcsData
        {
            public bool FullMagnitude;
            public Joystick Value;
        }

        public struct JoystickXData : IOneLabEcsData
        {
            public bool FullMagnitude;
            public Joystick Value;
        }
        
        public struct TransformData : IOneLabEcsData
        {
            public Transform Value;
            public void InitializeValues(Transform value)
            {
                Value = value;
            }
        }

        public struct RigidBody2DData : IOneLabEcsData
        {
            public Rigidbody2D Value;
        }

        public struct RigidBody3DData : IOneLabEcsData
        {
            public Rigidbody Value;
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

        public struct AlphaColorProcessData : IOneLabEcsData
        {
            public AlphaColorComponent AlphaColor;
            public float Speed;
            public UnityEvent<int, Componenter, OneLabPooler> OnSuccess;
        }
        
        public struct CharacterAnimatorExpendedData : IOneLabEcsData
        {
            public bool IsOneShot;
            public int FrameRemaining;
            public CharacterAnimatorComponent.SpritePack CurrentPack;
            public int CurrentSprite;
            public CharacterAnimatorComponent Value;
        }

        public struct CompositeObjectData : IOneLabEcsData
        {
            public CompositeObjectComponent Value;
        }

        public struct JumpData : IOneLabEcsData
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

        public struct KeyColliderSwitcherData : IOneLabEcsData
        {
            public KeyColliderSwitcherComponent Value;
        }

        public struct KeyboardPlatformInputMoverData : IOneLabEcsData
        {
            public float Speed;
            public bool FullSpeed;
            public bool UsePhysicalBody;
            public bool StopXWithoutInput;
            public Collider2D CharacterCollider2D;
            public bool HasXJoystick;
            public bool HasYJoystick;
        }

        public struct PointMoverData : IOneLabEcsData
        {
            public float Speed;
            public Vector2 StartPoint;
            public Vector2 EndPoint;
            public bool ToEndPoint;
        }

        public struct PointRotatorData : IOneLabEcsData
        {
            public Vector2 Point;
            public float Speed;
        }

        public struct RotatorMouseData : IOneLabEcsData
        {
            public float Speed;
        }

        public struct SelfRotatorXData : IOneLabEcsData
        {
            public float Speed;
        }

        public struct SelfRotatorYData : IOneLabEcsData
        {
            public float Speed;
        }

        public struct SelfRotatorZData : IOneLabEcsData
        {
            public float Speed;
        }

        public struct SmartCameraData : IOneLabEcsData
        {
            public Transform Transform;
            public float SmoothingSpeed;
            public Vector3 Velocity;
            public float SmoothingTime;
        }

        public struct SorterData : IOneLabEcsData
        {
            public SortRendererComponent Value;
        }

        public struct SpeedLimitXData : IOneLabEcsData
        {
            public float Limit;
        }

        public struct SpeedLimitYData : IOneLabEcsData 
        {
            public float Limit;
        }

        public struct TouchableData : IOneLabEcsData
        {
            public TouchableComponent Value;
        }

        public struct CharacterAnimatorData : IOneLabEcsData
        {
            public bool IsOneShot;
            public int FrameRemaining;
            public UniversalAnimatorComponent.SpritePack CurrentPack;
            public int CurrentSprite;
            public UniversalAnimatorComponent Value;
        }

        public struct VfxData : IOneLabEcsData
        {
            public VfxComponent Vfx;
            public int FramesRemaining;
            public float LoopTimeRemaining;
        }

        public struct CommandReleaseVfxMark : IOneLabEcsData
        {
        
        }

        public struct VisualData : IOneLabEcsData
        {
            public SpriteRenderer SpriteRenderer;
        }

        public struct TagsData : IOneLabEcsData
        {
            public string[] Values;
        }
    
        public struct CameraTargetData : IOneLabEcsData
        {
            public bool FollowX;
            public bool FollowY;
            public Vector3 Offset;
            public Transform Transform;
        }

        public struct AnimationInputData : IOneLabEcsData
        {
            public float HorizontalAxis;
        }

        public struct InputData : IOneLabEcsData
        {
            public float Horizontal;
            public float Vertical;
        }

        public struct EntityDeathTriggerData : IOneLabEcsData
        {
            public UnityEvent<int, Componenter, OneLabPooler> OnDead;
        }

        public struct GlobalTriggerData : IOneLabEcsData
        {
            public string[] Tags;
            public GlobalTrigger Value;
        }

        public struct KeyPressedTriggerData : IOneLabEcsData
        {
            public KeyCode Key;
            public UnityEvent<int, Componenter, OneLabPooler> OnPressed;
        }

        public struct TimerTriggerData : IOneLabEcsData
        {
            public UnityEvent<int, Componenter, OneLabPooler> OnTick;
            public float Delay;
            public float Timer;
            public bool IsLoop;
        }

        public struct FliperData : IOneLabEcsData
        {
            public FliperComponent Value;
        }

        public struct EcsMonoBehaviorData : IOneLabEcsData
        {
            public IEcsMonoBehavior Value;
        
            public void InitializeValues(IEcsMonoBehavior value)
            {
                Value = value;
            }
        }

        public struct DirectionMoverData : IOneLabEcsData
        {
            
        }
    }
}