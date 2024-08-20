
using Exerussus._1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class KeyColliderSwitcherSystem : OneLabSystem
    {
        private EcsFilter _keyColliderSwitcherFilter;
        private EcsFilter _joystickYFilter;

        protected override void Initialize()
        {
            _keyColliderSwitcherFilter = Componenter.Filter<OneLabData.KeyColliderSwitcher>().End();
            _joystickYFilter = Componenter.Filter<OneLabData.JoystickY>().End();
        }

        protected override void Update()
        {
            var joystickDown = false;
            
            foreach (var joystickEntity in _joystickYFilter)
            {
                ref var joystickData = ref Pooler.JoystickY.Get(joystickEntity);
                joystickDown = joystickData.Value.Vertical < -0.3f;
            }
            
            foreach (var entity in _keyColliderSwitcherFilter)
            {
                ref var keyColliderSwitcherData = ref Pooler.KeyColliderSwitcher.Get(entity);
                
                if ((joystickDown && keyColliderSwitcherData.Value.useJoystickY) || Input.GetKey(keyColliderSwitcherData.Value.key1) || Input.GetKey(keyColliderSwitcherData.Value.key2))
                {
                    keyColliderSwitcherData.Value._collider2D.isTrigger = keyColliderSwitcherData.Value.deactivateOnExist;
                }
                else
                {
                    if (!keyColliderSwitcherData.Value.isPlayerExist) keyColliderSwitcherData.Value._collider2D.isTrigger = !keyColliderSwitcherData.Value.deactivateOnExist;
                }
            }
        }
    }
}