
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    [AddComponentMenu("1Lab/Components/Fliper")]
    public class FliperComponent : OneLabComponent
    {
        [SerializeField] private ControlType mode;
        [SerializeField] private bool flipY;
        [SerializeField] private bool fliped;

        public ControlType Mode => mode;
        public bool Fliped => fliped;
        public bool FlipY => flipY;

        public override void Initialize()
        {
            Run();
        }

        public override void Destroy()
        {
            Stop();
        }

        public void Run()
        {
            ref var fliperData = ref Componenter.AddOrGet<FliperData>(Entity);
            fliperData.Value = this;
        }

        public void Stop()
        {
            Componenter.Del<FliperData>(Entity);
        }

        public void Flip(bool value)
        {
            if (value)
            {
                fliped = true;
                var localScale = transform.localScale;
                localScale.x = -localScale.x;
                if (flipY) localScale.y = -localScale.y;
                transform.localScale = localScale;
                
            }
            else
            {
                fliped = false;
                var localScale = transform.localScale;
                localScale.x = Mathf.Abs(localScale.x);
                if (flipY) localScale.y = Mathf.Abs(localScale.y);
                transform.localScale = localScale;
            }
        }

        public enum ControlType
        {
            Mouse,
            Axis,
            None
        }
    }

    public struct FliperData : IEcsComponent
    {
        public FliperComponent Value;
    }
}