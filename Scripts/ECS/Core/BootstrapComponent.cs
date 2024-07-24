using OneLab.Scripts.ECS.Core;
using Source.SignalSystem;
using UnityEngine;

namespace OneLab.Scripts.ECS.Core
{
    public abstract class BootstrapComponent : MonoBehaviour, IGameShareItem
    {
        protected GameShare GameShare;

        public Signal Signal { get; private set; }

        public void PreInit(GameShare gameShare)
        {
            GameShare = gameShare;
            Signal = gameShare.GetSharedObject<Signal>();
            OnPreInit();
        }
        
        protected virtual void OnPreInit() {}
        
        public abstract void Initialize();
    }
}