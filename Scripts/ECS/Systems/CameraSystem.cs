using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Lab.Scripts.ECS.Components;
using Exerussus._1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class CameraSystem : EcsSignalListener<IOneLabEcsData, CommandCameraFollowTransformSignal>
    {
        private EcsFilter _cameraFilter;
        private EcsFilter _targetFilter;

        protected override void Initialize()
        {
            _cameraFilter = Componenter.Filter<SmartCameraData>().End();
            _targetFilter = Componenter.Filter<CameraTargetData>().End();
        }

        protected override void Update()
        {
            foreach (var cameraEntity in _cameraFilter)
            {
                foreach (var targetEntity in _targetFilter)
                {
                    ref var cameraData = ref Componenter.Get<SmartCameraData>(cameraEntity);
                    ref var targetData = ref Componenter.Get<CameraTargetData>(targetEntity);
                    var cameraPos = cameraData.Transform.transform.position;
                    
                    var position = targetData.Transform.position + targetData.Offset;
                    position.z = cameraPos.z;
                    if (!targetData.FollowX) position.x = cameraPos.x;
                    if (!targetData.FollowY) position.y = cameraPos.y;

                    cameraData.Transform.transform.position = position;
                    return;
                }
            }
        }
        
        protected override void OnSignal(CommandCameraFollowTransformSignal data)
        {
            foreach (var entity in _targetFilter) Componenter.Del<CameraTargetData>(entity);
            
            TryInitCamera();
            
            ref var cameraTargetData = ref Componenter.AddOrGet<CameraTargetData>(data.TargetEntity);
            cameraTargetData.Transform = Componenter.Get<TransformData>(data.TargetEntity).Value;
            cameraTargetData.FollowY = data.FollowY;
            cameraTargetData.FollowX = data.FollowX;
            cameraTargetData.Offset = data.Offset;
        }

        private void TryInitCamera()
        {
            foreach (var cameraEntity in _cameraFilter) return;
            
            var camera = Camera.main;
            if (camera != null)
            {
                var cameraEntity = Componenter.GetNewEntity();
                ref var transformData = ref Componenter.TransformPool.Add(cameraEntity);
                transformData.Value = camera.transform;
                ref var smartCameraData = ref Componenter.AddOrGet<SmartCameraData>(cameraEntity);
                smartCameraData.Transform = camera.transform;
            }
        }
    }
    
    public struct CameraTargetData : IOneLabEcsData
    {
        public bool FollowX;
        public bool FollowY;
        public Vector3 Offset;
        public Transform Transform;
    }
}