using Exerussus._1Lab.Scripts.ECS.Core;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class SpawnSystem : OneLabEcsListener<OneLabSignals.CommandSpawnObject>
    {
        protected override void OnSignal(OneLabSignals.CommandSpawnObject data)
        {
            
        }
    }
}