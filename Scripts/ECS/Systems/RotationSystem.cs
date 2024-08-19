
using Exerussus._1EasyEcs.Scripts.Core;
using Exerussus._1Extensions.Scripts.Extensions;
using Exerussus._1Lab.Scripts.ECS.Core;
using Leopotam.EcsLite;
using UnityEngine;

namespace Exerussus._1Lab.Scripts.ECS.Systems
{
    public class RotationSystem : EasySystem
    {
        private EcsFilter _rotationMouseFilter;
        private EcsFilter _selfRotationXFilter;
        private EcsFilter _selfRotationYFilter;
        private EcsFilter _selfRotationZFilter;
        private EcsFilter _pointRotationFilter;
        private Camera _camera;
        private OneLabPooler _pooler;

        protected override void Initialize()
        {
            _camera = Camera.main;
            _rotationMouseFilter = Componenter.Filter<OneLabData.RotatorMouseData>().End();
            _selfRotationXFilter = Componenter.Filter<OneLabData.SelfRotatorXData>().End();
            _selfRotationYFilter = Componenter.Filter<OneLabData.SelfRotatorYData>().End();
            _selfRotationZFilter = Componenter.Filter<OneLabData.SelfRotatorZData>().End();
            _pointRotationFilter = Componenter.Filter<OneLabData.PointRotatorData>().End();
            GameShare.GetSharedObject(ref _pooler);
        }

        protected override void Update()
        {
            _rotationMouseFilter.Foreach(OnRotateMouseUpdate);
            _selfRotationXFilter.Foreach(OnSelfRotateXUpdate);
            _selfRotationYFilter.Foreach(OnSelfRotateYUpdate);
            _selfRotationZFilter.Foreach(OnSelfRotateZUpdate);
            _pointRotationFilter.Foreach(OnPointRotateUpdate);
        }

        private void OnPointRotateUpdate(int entity)
        {
            ref var transformData = ref _pooler.Transform.Get(entity);
            ref var pointRotatorData = ref _pooler.PointRotator.Get(entity);
            transformData.Value.RotateAround(pointRotatorData.Point, Vector3.forward, pointRotatorData.Speed * Time.fixedDeltaTime);
        }

        private void OnSelfRotateXUpdate(int entity)
        {
            ref var transformData = ref _pooler.Transform.Get(entity);
            ref var selfRotatorData = ref _pooler.SelfRotatorX.Get(entity);
            
            transformData.Value.Rotate(selfRotatorData.Speed * Time.deltaTime, 0, 0);
        }

        private void OnSelfRotateYUpdate(int entity)
        {
            ref var transformData = ref _pooler.Transform.Get(entity);
            ref var selfRotatorData = ref _pooler.SelfRotatorY.Get(entity);
            
            transformData.Value.Rotate(0, selfRotatorData.Speed * Time.deltaTime, 0);
        }

        private void OnSelfRotateZUpdate(int entity)
        {
            ref var transformData = ref _pooler.Transform.Get(entity);
            ref var selfRotatorData = ref _pooler.SelfRotatorZ.Get(entity);
            
            transformData.Value.Rotate(0, 0, selfRotatorData.Speed * Time.deltaTime);
        }

        private void OnRotateMouseUpdate(int entity)
        {
            ref var rotatorMouseData = ref _pooler.RotatorMouse.Get(entity);
            ref var transformData = ref _pooler.Transform.Get(entity);

            var mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            var mousePosition2D = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);
            var direction = (mousePosition2D - (Vector2)transformData.Value.position).normalized;
            
            transformData.Value.rotation = direction.ToQuaternion();
        }
    }
}