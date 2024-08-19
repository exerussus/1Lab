using Exerussus._1Lab.Scripts.ECS.Core;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Components
{
    public class DirectionMover : OneLabComponent
    {
        public bool autoRun;
        public Vector2 direction;
        public Vector2 speed;

        public override void Initialize()
        {
            if (autoRun) Run();
        }

        public void Run()
        {
            
        }

        public void Stop()
        {
            
        }
    }
}