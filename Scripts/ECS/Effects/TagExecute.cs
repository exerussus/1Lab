
using Exerussus._1Lab.Scripts.ECS.Core;
using Exerussus._1Lab.Scripts.ECS.Effects;
using UnityEngine.Events;

namespace Plugins.Exerussus._1Lab.Scripts.ECS.Effects
{
    public class TagExecute : EcsEffect
    {
        public string[] any;
        public string[] include;
        public string[] exclude;
        public UnityEvent<int, OneLabPooler> onSuccess;
        
        public void Execute(int originEntity, OneLabPooler pooler)
        {
            
        }
        
        public void Execute(int originEntity, int targetEntity, OneLabPooler pooler)
        {
            
        }
    }
}