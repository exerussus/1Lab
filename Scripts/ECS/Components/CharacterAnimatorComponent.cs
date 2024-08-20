using System;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/CharacterAnimator")]
    public class CharacterAnimatorComponent : OneLabComponent
    {
        public bool autoRun = true;
        public SpriteRenderer spriteRenderer;
        public SpritePack idle;
        public SpritePack fall;
        public SpritePack jump;
        public SpritePack run;
        public bool isFalling;
        public Collider2D groundCollider2D;
        public bool isTouchingCollider;
        public string[] tags;

        public override void Initialize()
        {
            if (autoRun) Run();
        }

        public void PlayJump()
        {
            var spritePack = jump;
            ref var characterAnimatorData = ref Pooler.CharacterAnimatorExpended.AddOrGet(Entity);
            characterAnimatorData.CurrentSprite = 0;
            characterAnimatorData.CurrentPack = spritePack;
            characterAnimatorData.FrameRemaining = spritePack.frameDelay;
            characterAnimatorData.IsOneShot = true;
            spriteRenderer.sprite = spritePack.sprites[characterAnimatorData.CurrentSprite];
        }
        
        public void Run()
        {
            ref var characterAnimatorData = ref Pooler.CharacterAnimatorExpended.AddOrGet(Entity);
            characterAnimatorData.Value = this;
            characterAnimatorData.CurrentSprite = 0;
            characterAnimatorData.CurrentPack = characterAnimatorData.Value.idle;
            characterAnimatorData.IsOneShot = false;
            ref var animationInputData = ref Pooler.AnimationInput.AddOrGet(Entity);
            animationInputData.HorizontalAxis = 0;
        }

        public void Stop()
        {
            spriteRenderer.sprite = idle.sprites[0];
            Pooler.CharacterAnimatorExpended.Del(Entity);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            foreach (var targetTag in tags) if (other.collider.CompareTag(targetTag))
            {
                groundCollider2D = other.collider;
                isTouchingCollider = true;
                isFalling = false;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            foreach (var targetTag in tags) if (other.collider.CompareTag(targetTag))
            {
                groundCollider2D = null;
                isTouchingCollider = false;
                isFalling = true;
            }
        }

        [Serializable]
        public class SpritePack
        {
            public int frameDelay;
            public Sprite[] sprites;
        }
    }
}