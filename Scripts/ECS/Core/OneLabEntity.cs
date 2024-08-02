
using Exerussus._1EasyEcs.Scripts.Core;

namespace Exerussus._1Lab.Scripts.ECS.Core
{
    public class OneLabEntity : EcsMonoBehavior
    {
        public void Start()
        {
            Initialize(OneLab.Componenter, OneLab.Signal);
        }
    }
}