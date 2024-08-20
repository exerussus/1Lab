
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public class OneLabPooler
    {
        public OneLabPooler(EcsWorld world)
        {
            #region Init
            
            Transform = new PoolerModule<OneLabData.Transform>(world);
            AlphaColorProcess = new PoolerModule<OneLabData.AlphaColorProcess>(world);
            CharacterAnimatorExpended = new PoolerModule<OneLabData.CharacterAnimatorExpended>(world);
            CompositeObject = new PoolerModule<OneLabData.CompositeObject>(world);
            Fliper = new PoolerModule<OneLabData.Fliper>(world);
            JoystickX = new PoolerModule<OneLabData.JoystickX>(world);
            JoystickY = new PoolerModule<OneLabData.JoystickY>(world);
            Jump = new PoolerModule<OneLabData.Jump>(world);
            KeyColliderSwitcher = new PoolerModule<OneLabData.KeyColliderSwitcher>(world);
            KeyboardPlatformInputMover = new PoolerModule<OneLabData.KeyboardPlatformInputMover>(world);
            PointMover = new PoolerModule<OneLabData.PointMover>(world);
            PointRotator = new PoolerModule<OneLabData.PointRotator>(world);
            RotatorMouse = new PoolerModule<OneLabData.RotatorMouse>(world);
            SelfRotatorX = new PoolerModule<OneLabData.SelfRotatorX>(world);
            SelfRotatorY = new PoolerModule<OneLabData.SelfRotatorY>(world);
            SelfRotatorZ = new PoolerModule<OneLabData.SelfRotatorZ>(world);
            SmartCamera = new PoolerModule<OneLabData.SmartCamera>(world);
            Sorter = new PoolerModule<OneLabData.Sorter>(world);
            SpeedLimitX = new PoolerModule<OneLabData.SpeedLimitX>(world);
            SpeedLimitY = new PoolerModule<OneLabData.SpeedLimitY>(world);
            Touchable = new PoolerModule<OneLabData.Touchable>(world);
            CharacterAnimator = new PoolerModule<OneLabData.CharacterAnimator>(world);
            Vfx = new PoolerModule<OneLabData.Vfx>(world);
            CommandReleaseVfx = new PoolerModule<OneLabData.CommandReleaseVfxMark>(world);
            Visual = new PoolerModule<OneLabData.Visual>(world);
            Tags = new PoolerModule<OneLabData.Tags>(world);
            CameraTarget = new PoolerModule<OneLabData.CameraTarget>(world);
            AnimationInput = new PoolerModule<OneLabData.AnimationInput>(world);
            Input = new PoolerModule<OneLabData.Input>(world);
            EntityDeathTrigger = new PoolerModule<OneLabData.EntityDeathTrigger>(world);
            GlobalTrigger = new PoolerModule<OneLabData.GlobalTrigger>(world);
            KeyPressedTrigger = new PoolerModule<OneLabData.KeyPressedTrigger>(world);
            TimerTrigger = new PoolerModule<OneLabData.TimerTrigger>(world);
            RigidBody2D = new PoolerModule<OneLabData.RigidBody2D>(world);
            RigidBody3D = new PoolerModule<OneLabData.RigidBody3D>(world);
            DirectionMover = new PoolerModule<OneLabData.DirectionMover>(world);
            OnDestroy = new PoolerModule<OneLabData.OnDestroy>(world);
            EcsMonoBehavior = new PoolerModule<OneLabData.EcsMonoBehavior>(world);

            #endregion
        }

        #region Properties

        public readonly PoolerModule<OneLabData.Transform> Transform;
        public readonly PoolerModule<OneLabData.AlphaColorProcess> AlphaColorProcess;
        public readonly PoolerModule<OneLabData.CharacterAnimatorExpended> CharacterAnimatorExpended;
        public readonly PoolerModule<OneLabData.CompositeObject> CompositeObject;
        public readonly PoolerModule<OneLabData.Fliper> Fliper;
        public readonly PoolerModule<OneLabData.JoystickX> JoystickX;
        public readonly PoolerModule<OneLabData.JoystickY> JoystickY;
        public readonly PoolerModule<OneLabData.Jump> Jump;
        public readonly PoolerModule<OneLabData.KeyColliderSwitcher> KeyColliderSwitcher;
        public readonly PoolerModule<OneLabData.KeyboardPlatformInputMover> KeyboardPlatformInputMover;
        public readonly PoolerModule<OneLabData.PointMover> PointMover;
        public readonly PoolerModule<OneLabData.PointRotator> PointRotator;
        public readonly PoolerModule<OneLabData.RotatorMouse> RotatorMouse;
        public readonly PoolerModule<OneLabData.SelfRotatorX> SelfRotatorX;
        public readonly PoolerModule<OneLabData.SelfRotatorY> SelfRotatorY;
        public readonly PoolerModule<OneLabData.SelfRotatorZ> SelfRotatorZ;
        public readonly PoolerModule<OneLabData.SmartCamera> SmartCamera;
        public readonly PoolerModule<OneLabData.Sorter> Sorter;
        public readonly PoolerModule<OneLabData.SpeedLimitX> SpeedLimitX;
        public readonly PoolerModule<OneLabData.SpeedLimitY> SpeedLimitY;
        public readonly PoolerModule<OneLabData.Touchable> Touchable;
        public readonly PoolerModule<OneLabData.CharacterAnimator> CharacterAnimator;
        public readonly PoolerModule<OneLabData.Vfx> Vfx;
        public readonly PoolerModule<OneLabData.CommandReleaseVfxMark> CommandReleaseVfx;
        public readonly PoolerModule<OneLabData.Visual> Visual;
        public readonly PoolerModule<OneLabData.Tags> Tags;
        public readonly PoolerModule<OneLabData.CameraTarget> CameraTarget;
        public readonly PoolerModule<OneLabData.AnimationInput> AnimationInput;
        public readonly PoolerModule<OneLabData.Input> Input;
        public readonly PoolerModule<OneLabData.EntityDeathTrigger> EntityDeathTrigger;
        public readonly PoolerModule<OneLabData.GlobalTrigger> GlobalTrigger;
        public readonly PoolerModule<OneLabData.KeyPressedTrigger> KeyPressedTrigger;
        public readonly PoolerModule<OneLabData.TimerTrigger> TimerTrigger;
        public readonly PoolerModule<OneLabData.RigidBody2D> RigidBody2D;
        public readonly PoolerModule<OneLabData.RigidBody3D> RigidBody3D;
        public readonly PoolerModule<OneLabData.DirectionMover> DirectionMover;
        public readonly PoolerModule<OneLabData.OnDestroy> OnDestroy;
        public readonly PoolerModule<OneLabData.EcsMonoBehavior> EcsMonoBehavior;

        #endregion
    }
}