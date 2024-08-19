
using Exerussus._1EasyEcs.Scripts.Core;
using Leopotam.EcsLite;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public class OneLabPooler
    {
        public OneLabPooler(EcsWorld world)
        {
            #region Init
            
            Transform = new PoolerModule<OneLabData.TransformData>(world);
            AlphaColorProcess = new PoolerModule<OneLabData.AlphaColorProcessData>(world);
            CharacterAnimatorExpended = new PoolerModule<OneLabData.CharacterAnimatorExpendedData>(world);
            CompositeObject = new PoolerModule<OneLabData.CompositeObjectData>(world);
            Fliper = new PoolerModule<OneLabData.FliperData>(world);
            JoystickX = new PoolerModule<OneLabData.JoystickXData>(world);
            JoystickY = new PoolerModule<OneLabData.JoystickYData>(world);
            Jump = new PoolerModule<OneLabData.JumpData>(world);
            KeyColliderSwitcher = new PoolerModule<OneLabData.KeyColliderSwitcherData>(world);
            KeyboardPlatformInputMover = new PoolerModule<OneLabData.KeyboardPlatformInputMoverData>(world);
            PointMover = new PoolerModule<OneLabData.PointMoverData>(world);
            PointRotator = new PoolerModule<OneLabData.PointRotatorData>(world);
            RotatorMouse = new PoolerModule<OneLabData.RotatorMouseData>(world);
            SelfRotatorX = new PoolerModule<OneLabData.SelfRotatorXData>(world);
            SelfRotatorY = new PoolerModule<OneLabData.SelfRotatorYData>(world);
            SelfRotatorZ = new PoolerModule<OneLabData.SelfRotatorZData>(world);
            SmartCamera = new PoolerModule<OneLabData.SmartCameraData>(world);
            Sorter = new PoolerModule<OneLabData.SorterData>(world);
            SpeedLimitX = new PoolerModule<OneLabData.SpeedLimitXData>(world);
            SpeedLimitY = new PoolerModule<OneLabData.SpeedLimitYData>(world);
            Touchable = new PoolerModule<OneLabData.TouchableData>(world);
            CharacterAnimator = new PoolerModule<OneLabData.CharacterAnimatorData>(world);
            Vfx = new PoolerModule<OneLabData.VfxData>(world);
            CommandReleaseVfx = new PoolerModule<OneLabData.CommandReleaseVfxMark>(world);
            Visual = new PoolerModule<OneLabData.VisualData>(world);
            Tags = new PoolerModule<OneLabData.TagsData>(world);
            CameraTarget = new PoolerModule<OneLabData.CameraTargetData>(world);
            AnimationInput = new PoolerModule<OneLabData.AnimationInputData>(world);
            Input = new PoolerModule<OneLabData.InputData>(world);
            EntityDeathTrigger = new PoolerModule<OneLabData.EntityDeathTriggerData>(world);
            GlobalTrigger = new PoolerModule<OneLabData.GlobalTriggerData>(world);
            KeyPressedTrigger = new PoolerModule<OneLabData.KeyPressedTriggerData>(world);
            TimerTrigger = new PoolerModule<OneLabData.TimerTriggerData>(world);
            RigidBody2D = new PoolerModule<OneLabData.RigidBody2DData>(world);
            DirectionMover = new PoolerModule<OneLabData.DirectionMoverData>(world);

            #endregion
        }

        #region Properties

        public readonly PoolerModule<OneLabData.TransformData> Transform;
        public readonly PoolerModule<OneLabData.AlphaColorProcessData> AlphaColorProcess;
        public readonly PoolerModule<OneLabData.CharacterAnimatorExpendedData> CharacterAnimatorExpended;
        public readonly PoolerModule<OneLabData.CompositeObjectData> CompositeObject;
        public readonly PoolerModule<OneLabData.FliperData> Fliper;
        public readonly PoolerModule<OneLabData.JoystickXData> JoystickX;
        public readonly PoolerModule<OneLabData.JoystickYData> JoystickY;
        public readonly PoolerModule<OneLabData.JumpData> Jump;
        public readonly PoolerModule<OneLabData.KeyColliderSwitcherData> KeyColliderSwitcher;
        public readonly PoolerModule<OneLabData.KeyboardPlatformInputMoverData> KeyboardPlatformInputMover;
        public readonly PoolerModule<OneLabData.PointMoverData> PointMover;
        public readonly PoolerModule<OneLabData.PointRotatorData> PointRotator;
        public readonly PoolerModule<OneLabData.RotatorMouseData> RotatorMouse;
        public readonly PoolerModule<OneLabData.SelfRotatorXData> SelfRotatorX;
        public readonly PoolerModule<OneLabData.SelfRotatorYData> SelfRotatorY;
        public readonly PoolerModule<OneLabData.SelfRotatorZData> SelfRotatorZ;
        public readonly PoolerModule<OneLabData.SmartCameraData> SmartCamera;
        public readonly PoolerModule<OneLabData.SorterData> Sorter;
        public readonly PoolerModule<OneLabData.SpeedLimitXData> SpeedLimitX;
        public readonly PoolerModule<OneLabData.SpeedLimitYData> SpeedLimitY;
        public readonly PoolerModule<OneLabData.TouchableData> Touchable;
        public readonly PoolerModule<OneLabData.CharacterAnimatorData> CharacterAnimator;
        public readonly PoolerModule<OneLabData.VfxData> Vfx;
        public readonly PoolerModule<OneLabData.CommandReleaseVfxMark> CommandReleaseVfx;
        public readonly PoolerModule<OneLabData.VisualData> Visual;
        public readonly PoolerModule<OneLabData.TagsData> Tags;
        public readonly PoolerModule<OneLabData.CameraTargetData> CameraTarget;
        public readonly PoolerModule<OneLabData.AnimationInputData> AnimationInput;
        public readonly PoolerModule<OneLabData.InputData> Input;
        public readonly PoolerModule<OneLabData.EntityDeathTriggerData> EntityDeathTrigger;
        public readonly PoolerModule<OneLabData.GlobalTriggerData> GlobalTrigger;
        public readonly PoolerModule<OneLabData.KeyPressedTriggerData> KeyPressedTrigger;
        public readonly PoolerModule<OneLabData.TimerTriggerData> TimerTrigger;
        public readonly PoolerModule<OneLabData.RigidBody2DData> RigidBody2D;
        public readonly PoolerModule<OneLabData.DirectionMoverData> DirectionMover;

        #endregion
    }
}