using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Components;
using Exerussus._1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class CharacterAnimatorSystem : EasySystem
    {
        public EcsFilter _characterAnimatorFilter;
        public EcsFilter _characterAnimatorExpendedFilter;

        protected override void Initialize()
        {
            _characterAnimatorFilter = Componenter.Filter<CharacterAnimatorData>().End();
            _characterAnimatorExpendedFilter = Componenter.Filter<CharacterAnimatorExpendedData>().Inc<AnimationInputData>().End();
        }

        protected override void Update()
        {
            _characterAnimatorFilter.Foreach(OnCharacterAnimatorUpdate);
            _characterAnimatorExpendedFilter.Foreach(OnCharacterAnimatorExpendedUpdate);
        }

        private void OnCharacterAnimatorExpendedUpdate(int entity)
        {
            ref var characterAnimatorData = ref Componenter.Get<CharacterAnimatorExpendedData>(entity);
            
            var input = Componenter.Get<AnimationInputData>(entity).HorizontalAxis;

            if (input != 0) characterAnimatorData.Value.spriteRenderer.flipX = input < 0;

            if (characterAnimatorData.Value.isTouchingCollider)
            {
                if (characterAnimatorData.Value.groundCollider2D.isTrigger)
                {
                    characterAnimatorData.Value.isFalling = true;
                    characterAnimatorData.Value.isTouchingCollider = false;
                }
            }
            
            if (characterAnimatorData.Value.isFalling)
            {
                if (characterAnimatorData.CurrentPack != characterAnimatorData.Value.fall)
                {
                    characterAnimatorData.FrameRemaining = characterAnimatorData.Value.fall.frameDelay;
                    characterAnimatorData.CurrentSprite = 0;
                    characterAnimatorData.CurrentPack = characterAnimatorData.Value.fall;
                    characterAnimatorData.Value.spriteRenderer.sprite = characterAnimatorData.Value.fall.sprites[characterAnimatorData.CurrentSprite];
                }
                return;
            }
            
            if (characterAnimatorData.IsOneShot)
            {
                if (characterAnimatorData.FrameRemaining > 0)
                {
                    characterAnimatorData.FrameRemaining--;
                    return;
                }
                
                characterAnimatorData.FrameRemaining = characterAnimatorData.CurrentPack.frameDelay;
                characterAnimatorData.CurrentSprite++;
                if (characterAnimatorData.CurrentSprite >= characterAnimatorData.CurrentPack.sprites.Length)
                {
                    characterAnimatorData.IsOneShot = false;
                }
                else
                {
                    characterAnimatorData.Value.spriteRenderer.sprite = characterAnimatorData.CurrentPack.sprites[characterAnimatorData.CurrentSprite];
                    return; 
                }
            }
            
            if (input != 0)
            {
                characterAnimatorData.Value.spriteRenderer.flipX = input < 0;
                if (characterAnimatorData.CurrentPack != characterAnimatorData.Value.run)
                {
                    characterAnimatorData.CurrentPack = characterAnimatorData.Value.run;
                    characterAnimatorData.FrameRemaining = characterAnimatorData.CurrentPack.frameDelay;
                    characterAnimatorData.CurrentSprite = 0;
                    characterAnimatorData.Value.spriteRenderer.sprite = characterAnimatorData.CurrentPack.sprites[characterAnimatorData.CurrentSprite];
                }
                else
                {
                    if (characterAnimatorData.FrameRemaining > 0)
                    {
                        characterAnimatorData.FrameRemaining--;
                        return;
                    }
                        
                    characterAnimatorData.FrameRemaining = characterAnimatorData.CurrentPack.frameDelay;
                    characterAnimatorData.CurrentSprite++;
                    if (characterAnimatorData.CurrentSprite >= characterAnimatorData.CurrentPack.sprites.Length) characterAnimatorData.CurrentSprite = 0;
                    characterAnimatorData.Value.spriteRenderer.sprite = characterAnimatorData.CurrentPack.sprites[characterAnimatorData.CurrentSprite];
                }
                    
                return;
            }

            if (characterAnimatorData.CurrentPack != characterAnimatorData.Value.idle)
            {
                characterAnimatorData.CurrentPack = characterAnimatorData.Value.idle;
                characterAnimatorData.FrameRemaining = characterAnimatorData.CurrentPack.frameDelay;
                characterAnimatorData.CurrentSprite = 0;
                characterAnimatorData.Value.spriteRenderer.sprite = characterAnimatorData.CurrentPack.sprites[characterAnimatorData.CurrentSprite];
            }
            else
            {
                if (characterAnimatorData.FrameRemaining > 0)
                {
                    characterAnimatorData.FrameRemaining--;
                    return;
                }

                characterAnimatorData.FrameRemaining = characterAnimatorData.CurrentPack.frameDelay;
                characterAnimatorData.CurrentSprite++;
                if (characterAnimatorData.CurrentSprite >= characterAnimatorData.CurrentPack.sprites.Length) characterAnimatorData.CurrentSprite = 0;
                characterAnimatorData.Value.spriteRenderer.sprite = characterAnimatorData.CurrentPack.sprites[characterAnimatorData.CurrentSprite];
            }
        }

        private void OnCharacterAnimatorUpdate(int entity)
        {
            ref var characterAnimatorData = ref Componenter.Get<CharacterAnimatorData>(entity);

            if (characterAnimatorData.IsOneShot)
            {
                if (characterAnimatorData.FrameRemaining > 0)
                {
                    characterAnimatorData.FrameRemaining--;
                    return;
                }
                
                characterAnimatorData.FrameRemaining = characterAnimatorData.CurrentPack.frameDelay;
                characterAnimatorData.CurrentSprite++;
                if (characterAnimatorData.CurrentSprite >= characterAnimatorData.CurrentPack.sprites.Length)
                {
                    characterAnimatorData.IsOneShot = false;
                }
                else
                {
                    characterAnimatorData.Value.spriteRenderer.sprite = characterAnimatorData.CurrentPack.sprites[characterAnimatorData.CurrentSprite];
                    return; 
                }
            }
            
            foreach (var spritePack in characterAnimatorData.Value.animations)
            {
                if (Input.GetKey(spritePack.key))
                {
                    if (characterAnimatorData.CurrentPack != spritePack)
                    {
                        characterAnimatorData.FrameRemaining = spritePack.frameDelay;
                        characterAnimatorData.CurrentSprite = 0;
                        characterAnimatorData.CurrentPack = spritePack;
                        characterAnimatorData.Value.spriteRenderer.sprite = spritePack.sprites[characterAnimatorData.CurrentSprite];
                        characterAnimatorData.Value.spriteRenderer.flipX = spritePack.isFlip;
                    }
                    else
                    {
                        if (characterAnimatorData.FrameRemaining > 0)
                        {
                            characterAnimatorData.FrameRemaining--;
                            return;
                        }
                        
                        characterAnimatorData.FrameRemaining = characterAnimatorData.CurrentPack.frameDelay;
                        characterAnimatorData.CurrentSprite++;
                        if (characterAnimatorData.CurrentSprite >= spritePack.sprites.Length) characterAnimatorData.CurrentSprite = 0;
                        characterAnimatorData.Value.spriteRenderer.sprite = spritePack.sprites[characterAnimatorData.CurrentSprite];
                    }
                    
                    return;
                }
            }

            if (characterAnimatorData.CurrentPack != characterAnimatorData.Value.idle)
            {
                characterAnimatorData.CurrentPack = characterAnimatorData.Value.idle;
                characterAnimatorData.FrameRemaining = characterAnimatorData.CurrentPack.frameDelay;
                characterAnimatorData.CurrentSprite = 0;
                characterAnimatorData.Value.spriteRenderer.sprite = characterAnimatorData.CurrentPack.sprites[characterAnimatorData.CurrentSprite];
            }
            else
            {
                if (characterAnimatorData.FrameRemaining > 0)
                {
                    characterAnimatorData.FrameRemaining--;
                    return;
                }

                characterAnimatorData.FrameRemaining = characterAnimatorData.CurrentPack.frameDelay;
                characterAnimatorData.CurrentSprite++;
                if (characterAnimatorData.CurrentSprite >= characterAnimatorData.CurrentPack.sprites.Length) characterAnimatorData.CurrentSprite = 0;
                characterAnimatorData.Value.spriteRenderer.sprite = characterAnimatorData.CurrentPack.sprites[characterAnimatorData.CurrentSprite];
            }
        }
    }

    public struct AnimationInputData : IEcsComponent
    {
        public float HorizontalAxis;
    }
}