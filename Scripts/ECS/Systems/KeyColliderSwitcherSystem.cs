using _1Lab.Scripts.ECS.Components;
using _1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace _1Lab.Scripts.ECS.Systems
{
    public class KeyColliderSwitcherSystem : EasySystem
    {
        private EcsFilter _keyColliderSwitcherFilter;
        protected override void Initialize()
        {
            _keyColliderSwitcherFilter = Componenter.Filter<KeyColliderSwitcherData>().End();
        }

        protected override void Update()
        {
            foreach (var entity in _keyColliderSwitcherFilter)
            {
                ref var keyColliderSwitcherData = ref Componenter.Get<KeyColliderSwitcherData>(entity);
                
                if (Input.GetKey(keyColliderSwitcherData.Value.key1) || Input.GetKey(keyColliderSwitcherData.Value.key2))
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