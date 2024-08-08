﻿using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class KeyColliderSwitcherSystem : EasySystem
    {
        private EcsFilter _keyColliderSwitcherFilter;
        private EcsFilter _joystickYFilter;
        
        protected override void Initialize()
        {
            _keyColliderSwitcherFilter = Componenter.Filter<KeyColliderSwitcherData>().End();
            _joystickYFilter = Componenter.Filter<JoystickYData>().End();
        }

        protected override void Update()
        {
            var joystickDown = false;
            
            foreach (var joystickEntity in _joystickYFilter)
            {
                ref var joystickData = ref Componenter.Get<JoystickYData>(joystickEntity);
                joystickDown = joystickData.Value.Vertical < -0.3f;
            }
            
            foreach (var entity in _keyColliderSwitcherFilter)
            {
                ref var keyColliderSwitcherData = ref Componenter.Get<KeyColliderSwitcherData>(entity);
                
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