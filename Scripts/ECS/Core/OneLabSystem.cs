using Exerussus._1EasyEcs.Scripts.Core;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public class OneLabSystem : EasySystem
    {
        protected OneLabPooler Pooler;
        
        public override void PreInit(GameShare gameShare, float tickTime, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, initializeType);
            gameShare.GetSharedObject(ref Pooler);
        }
    }
}