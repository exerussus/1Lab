
using System;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/UniversalAnimator")]
    public class UniversalAnimatorComponent : OneLabComponent
    {
        public bool autoRun = true;
        public SpriteRenderer spriteRenderer;
        public SpritePack idle;
        public AnimationSpritePack[] animations;

        public override void Initialize()
        {
            if (autoRun) Run();
        }

        public void InvokeOneShot(int packIndex)
        {
            var spritePack = animations[packIndex];
            ref var characterAnimatorData = ref Componenter.AddOrGet<OneLabData.CharacterAnimatorData>(Entity);
            characterAnimatorData.CurrentSprite = 0;
            characterAnimatorData.CurrentPack = spritePack;
            characterAnimatorData.FrameRemaining = spritePack.frameDelay;
            characterAnimatorData.IsOneShot = true;
            spriteRenderer.sprite = spritePack.sprites[characterAnimatorData.CurrentSprite];
        }
        
        public void Run()
        {
            ref var characterAnimatorData = ref Componenter.AddOrGet<OneLabData.CharacterAnimatorData>(Entity);
            characterAnimatorData.Value = this;
            characterAnimatorData.CurrentSprite = 0;
            characterAnimatorData.CurrentPack = characterAnimatorData.Value.idle;
            characterAnimatorData.IsOneShot = false;
        }

        public void Stop()
        {
            spriteRenderer.sprite = idle.sprites[0];
            Componenter.Del<OneLabData.CharacterAnimatorData>(Entity);
        }


        [Serializable]
        public class SpritePack
        {
            public int frameDelay;
            public Sprite[] sprites;
        }

        [Serializable]
        public class AnimationSpritePack : SpritePack
        {
            public KeyCode key;
            public bool isFlip;
        }
    }
}