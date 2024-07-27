using System;
using _1Lab.Scripts.ECS.Core;
using _1Lab.Scripts.ECS.Core.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace _1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/CharacterAnimator")]
    public class CharacterAnimatorComponent : EcsComponent
    {
        public bool autoRun = true;
        public SpriteRenderer spriteRenderer;
        [FormerlySerializedAs("idlePack")] public SpritePack idle;
        [FormerlySerializedAs("spritePacks")] public SpritePack[] animations;

        public override void Initialize()
        {
            if (autoRun) Run();
        }

        public void InvokeOneShot(int packIndex)
        {
            var spritePack = animations[packIndex];
            ref var characterAnimatorData = ref Componenter.AddOrGet<CharacterAnimatorData>(Entity);
            characterAnimatorData.CurrentSprite = 0;
            characterAnimatorData.CurrentPack = spritePack;
            characterAnimatorData.FrameRemaining = spritePack.frameDelay;
            characterAnimatorData.IsOneShot = true;
            spriteRenderer.sprite = spritePack.sprites[characterAnimatorData.CurrentSprite];
        }
        
        public void Run()
        {
            ref var characterAnimatorData = ref Componenter.AddOrGet<CharacterAnimatorData>(Entity);
            characterAnimatorData.Value = this;
            characterAnimatorData.CurrentSprite = 0;
            characterAnimatorData.CurrentPack = characterAnimatorData.Value.idle;
            characterAnimatorData.IsOneShot = false;
        }

        public void Stop()
        {
            spriteRenderer.sprite = idle.sprites[0];
            Componenter.Del<CharacterAnimatorData>(Entity);
        }

        [Serializable]
        public class SpritePack
        {
            public KeyCode key;
            public int frameDelay;
            public Sprite[] sprites;
            public bool isFlip;
        }
    }

    public struct CharacterAnimatorData : IEcsComponent
    {
        public bool IsOneShot;
        public int FrameRemaining;
        public CharacterAnimatorComponent.SpritePack CurrentPack;
        public int CurrentSprite;
        public CharacterAnimatorComponent Value;
    }
}