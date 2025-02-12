﻿
using Exerussus._1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using UnityEngine;
using LogType = Exerussus._1EasyEcs.Scripts.Custom.LogType;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class CameraSystem : OneLabEcsListener<OneLabSignals.CommandCameraFollowTransformSignal>
    {
        private EcsFilter _cameraFilter;
        private EcsFilter _targetFilter;

        protected override void Initialize()
        {
            _cameraFilter = Componenter.Filter<OneLabData.SmartCamera>().End();
            _targetFilter = Componenter.Filter<OneLabData.CameraTarget>().End();
        }

        protected override void Update()
        {
            foreach (var cameraEntity in _cameraFilter)
            {
                foreach (var targetEntity in _targetFilter)
                {
                    ref var cameraData = ref Pooler.SmartCamera.Get(cameraEntity);
                    ref var targetData = ref Pooler.CameraTarget.Get(targetEntity);
                    var cameraPos = cameraData.Transform.transform.position;
            
                    var targetPosition = targetData.Transform.position + targetData.Offset;
                    targetPosition.z = cameraPos.z;
                    if (!targetData.FollowX) targetPosition.x = cameraPos.x;
                    if (!targetData.FollowY) targetPosition.y = cameraPos.y;

                    // Используем SmoothDamp вместо Lerp для более плавного движения
                    var smoothedPosition = Vector3.SmoothDamp(
                        cameraPos, 
                        targetPosition, 
                        ref cameraData.Velocity, // Добавьте новое поле Velocity в SmartCameraData
                        cameraData.SmoothingTime // Добавьте SmoothingTime в SmartCameraData
                    );
            
                    cameraData.Transform.transform.position = smoothedPosition;
                    return;
                }
            }
        }
        
        protected override void OnSignal(OneLabSignals.CommandCameraFollowTransformSignal data)
        {
            foreach (var entity in _targetFilter) Pooler.CameraTarget.Del(entity);
            
            TryInitCamera();
            
            ref var cameraTargetData = ref Pooler.CameraTarget.AddOrGet(data.TargetEntity);
            cameraTargetData.Transform = Pooler.Transform.Get(data.TargetEntity).Value;
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
                ref var transformData = ref Pooler.Transform.Add(cameraEntity);
                transformData.Value = camera.transform;
                ref var smartCameraData = ref Pooler.SmartCamera.AddOrGet(cameraEntity);
                smartCameraData.Transform = camera.transform;
                smartCameraData.SmoothingSpeed = 5.95f;
                smartCameraData.SmoothingTime = 01.3f;
            }
        }
    }
}