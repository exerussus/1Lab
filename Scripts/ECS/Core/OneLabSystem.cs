using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.Data.GamesConfigurations;
using Leopotam.EcsLite;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public abstract class OneLabSystem : EasySystem<OneLabPooler>
    {
        protected OneLabConfiguration Configuration;
        protected TagsHandler TagsHandler;
        public override void PreInit(GameShare gameShare, float tickTime, EcsWorld world, InitializeType initializeType = InitializeType.None)
        {
            base.PreInit(gameShare, tickTime, world, initializeType);
            gameShare.GetSharedObject(ref Configuration);
            gameShare.GetSharedObject(ref TagsHandler);
        }
    }
}