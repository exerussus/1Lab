using _1Lab.Scripts.ECS.Components;
using _1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace _1Lab.Scripts.ECS.Systems
{
    public class CharacterAnimatorSystem : EasySystem
    {
        public EcsFilter _characterAnimatorFilter;

        protected override void Initialize()
        {
            _characterAnimatorFilter = Componenter.Filter<CharacterAnimatorData>().End();
        }

        protected override void Update()
        {
            _characterAnimatorFilter.Foreach(OnCharacterAnimatorUpdate);
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
}