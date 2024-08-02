
using Exerussus._1EasyEcs.Scripts.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public abstract class BootstrapComponent : MonoBehaviour
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