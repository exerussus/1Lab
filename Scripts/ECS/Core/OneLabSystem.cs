using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.Data.GamesConfigurations;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public abstract class OneLabSystem : EasySystem<OneLabPooler>
    {
        protected OneLabConfiguration Configuration;
        protected TagsHandler TagsHandler;
        public override void PreInit(GameShare gameShare, float tickTime, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, initializeType);
            gameShare.GetSharedObject(ref Configuration);
            gameShare.GetSharedObject(ref TagsHandler);
        }
    }
}