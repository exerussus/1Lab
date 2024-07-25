
using _1Lab.Scripts.ECS.Core;
using _1Lab.Scripts.ECS.Core.Interfaces;
using UnityEngine;

namespace _1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/Vfx")]
    public class VfxComponent : MonoBehaviour
    {
        public bool isLoop;
        public int frameDelay;
        public bool hasLoopTimer;
        public float loopTime;
        public SpriteRenderer spriteRenderer;
        public Sprite[] sprites;
        public int currentSprite;
        [HideInInspector] public GameObject prefab;
        private bool _running;
        
        public void OnRelease()
        {
            enabled = false;
            spriteRenderer.sprite = sprites[0];
            currentSprite = 0;
            _running = false;
        }

        public void Run()
        {
            if (_running) return;
            
            _running = true;
            currentSprite = 0;
            spriteRenderer.sprite = sprites[currentSprite];
        }
    }

    public struct VfxData : IEcsComponent
    {
        public VfxComponent Vfx;
        public int FramesRemaining;
        public float LoopTimeRemaining;
    }

    public struct CommandReleaseVfxMark
    {
        
    }
}