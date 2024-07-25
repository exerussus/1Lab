
using UnityEngine;

namespace _1Lab.Scripts.ECS.Core
{
    public abstract class BootstrapComponent : MonoBehaviour, IGameShareItem
    {
        protected GameShare GameShare;

        public void PreInit(GameShare gameShare)
        {
            GameShare = gameShare;
            OnPreInit();
        }
        
        protected virtual void OnPreInit() {}
        
        public abstract void Initialize();
    }
}