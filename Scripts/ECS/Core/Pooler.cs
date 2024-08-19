using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Components;
using Exerussus._1Lab.Scripts.ECS.Systems;
using Exerussus._1Lab.Scripts.ECS.Triggers;
using Leopotam.EcsLite;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public class Pooler
    {
        public Pooler(EcsWorld world)
        {
            #region Init
            
            Transform = new PoolerModule<TransformData>(world);
            AlphaColorProcess = new PoolerModule<AlphaColorProcessData>(world);
            CharacterAnimatorExpended = new PoolerModule<CharacterAnimatorExpendedData>(world);
            CompositeObject = new PoolerModule<CompositeObjectData>(world);
            Fliper = new PoolerModule<FliperData>(world);
            JoystickX = new PoolerModule<JoystickXData>(world);
            JoystickY = new PoolerModule<JoystickYData>(world);
            Jump = new PoolerModule<JumpData>(world);
            KeyColliderSwitcher = new PoolerModule<KeyColliderSwitcherData>(world);
            KeyboardPlatformInputMover = new PoolerModule<KeyboardPlatformInputMoverData>(world);
            PointMover = new PoolerModule<PointMoverData>(world);
            PointRotator = new PoolerModule<PointRotatorData>(world);
            RotatorMouse = new PoolerModule<RotatorMouseData>(world);
            SelfRotatorX = new PoolerModule<SelfRotatorXData>(world);
            SelfRotatorY = new PoolerModule<SelfRotatorYData>(world);
            SelfRotatorZ = new PoolerModule<SelfRotatorZData>(world);
            SmartCamera = new PoolerModule<SmartCameraData>(world);
            Sorter = new PoolerModule<SorterData>(world);
            SpeedLimitX = new PoolerModule<SpeedLimitXData>(world);
            SpeedLimitY = new PoolerModule<SpeedLimitYData>(world);
            Touchable = new PoolerModule<TouchableData>(world);
            CharacterAnimator = new PoolerModule<CharacterAnimatorData>(world);
            Vfx = new PoolerModule<VfxData>(world);
            CommandReleaseVfx = new PoolerModule<CommandReleaseVfxMark>(world);
            Visual = new PoolerModule<VisualData>(world);
            Tags = new PoolerModule<TagsData>(world);
            CameraTarget = new PoolerModule<CameraTargetData>(world);
            AnimationInput = new PoolerModule<AnimationInputData>(world);
            Input = new PoolerModule<InputData>(world);
            EntityDeathTrigger = new PoolerModule<EntityDeathTriggerData>(world);
            GlobalTrigger = new PoolerModule<GlobalTriggerData>(world);
            KeyPressedTrigger = new PoolerModule<KeyPressedTriggerData>(world);
            TimerTrigger = new PoolerModule<TimerTriggerData>(world);
            RigidBody2D = new PoolerModule<RigidBody2DData>(world);

            #endregion
        }

        #region Properties

        public readonly PoolerModule<TransformData> Transform;
        public readonly PoolerModule<AlphaColorProcessData> AlphaColorProcess;
        public readonly PoolerModule<CharacterAnimatorExpendedData> CharacterAnimatorExpended;
        public readonly PoolerModule<CompositeObjectData> CompositeObject;
        public readonly PoolerModule<FliperData> Fliper;
        public readonly PoolerModule<JoystickXData> JoystickX;
        public readonly PoolerModule<JoystickYData> JoystickY;
        public readonly PoolerModule<JumpData> Jump;
        public readonly PoolerModule<KeyColliderSwitcherData> KeyColliderSwitcher;
        public readonly PoolerModule<KeyboardPlatformInputMoverData> KeyboardPlatformInputMover;
        public readonly PoolerModule<PointMoverData> PointMover;
        public readonly PoolerModule<PointRotatorData> PointRotator;
        public readonly PoolerModule<RotatorMouseData> RotatorMouse;
        public readonly PoolerModule<SelfRotatorXData> SelfRotatorX;
        public readonly PoolerModule<SelfRotatorYData> SelfRotatorY;
        public readonly PoolerModule<SelfRotatorZData> SelfRotatorZ;
        public readonly PoolerModule<SmartCameraData> SmartCamera;
        public readonly PoolerModule<SorterData> Sorter;
        public readonly PoolerModule<SpeedLimitXData> SpeedLimitX;
        public readonly PoolerModule<SpeedLimitYData> SpeedLimitY;
        public readonly PoolerModule<TouchableData> Touchable;
        public readonly PoolerModule<CharacterAnimatorData> CharacterAnimator;
        public readonly PoolerModule<VfxData> Vfx;
        public readonly PoolerModule<CommandReleaseVfxMark> CommandReleaseVfx;
        public readonly PoolerModule<VisualData> Visual;
        public readonly PoolerModule<TagsData> Tags;
        public readonly PoolerModule<CameraTargetData> CameraTarget;
        public readonly PoolerModule<AnimationInputData> AnimationInput;
        public readonly PoolerModule<InputData> Input;
        public readonly PoolerModule<EntityDeathTriggerData> EntityDeathTrigger;
        public readonly PoolerModule<GlobalTriggerData> GlobalTrigger;
        public readonly PoolerModule<KeyPressedTriggerData> KeyPressedTrigger;
        public readonly PoolerModule<TimerTriggerData> TimerTrigger;
        public readonly PoolerModule<RigidBody2DData> RigidBody2D;

        #endregion
    }

    public interface IPoolerModule<T> where T : struct, IEcsComponent
    {
        public ref T AddOrGet(int entity);
        public ref T Add(int entity);
        public ref T Get(int entity);
        public bool Has(int entity);
        public void Del(int entity);
    }
    
    public class PoolerModule<T> : IPoolerModule<T> where T : struct, IEcsComponent
    {
        public PoolerModule(EcsWorld world)
        {
            _world = world;
            _pool = world.GetPool<T>();
        }

        private EcsWorld _world;
        private readonly EcsPool<T> _pool;

        public ref T AddOrGet(int entity)
        {
            if (_pool.Has(entity)) return ref _pool.Get(entity);
            return ref _pool.Add(entity);
        }

        public ref T Add(int entity)
        {
            return ref _pool.Add(entity);
        }

        public ref T Get(int entity)
        {
            return ref _pool.Get(entity);
        }

        public bool Has(int entity)
        {
            return _pool.Has(entity);
        }

        public void Del(int entity)
        {
            _pool.Del(entity);
        }
    }
}