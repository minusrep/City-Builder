using System;
using Runtime.Descriptions.CameraControl;
using Runtime.GameSystems;
using UnityEngine;

namespace Runtime.CameraControl
{
    public class CameraControlSystem : IGameSystem
    {
        public string Id => "camera_control";

        private float _currentZoomSpeed;
        private Vector2 _currentOrbitSpeed;
        private Vector3 _currentMoveSpeed;

        private readonly CameraControlModel _cameraControlModel;
        private readonly CameraControlView _cameraControlView;
        private readonly CameraControlDescription _cameraControlDescription;

        public CameraControlSystem(CameraControlModel cameraControlModel, CameraControlView cameraControlView,
            CameraControlDescription cameraControlDescription)
        {
            _cameraControlModel = cameraControlModel;
            _cameraControlView = cameraControlView;
            _cameraControlDescription = cameraControlDescription;
        }

        public void Update(float deltaTime)
        {
            if (_cameraControlModel.IsActive)
            {
                UpdateZoom(deltaTime);
                UpdateOrbit(deltaTime);
                UpdateMovement(deltaTime);
            }
        }

        private void UpdateZoom(float deltaTime)
        {
            var axis = _cameraControlView.OrbitalFollow.RadialAxis;

            var targetZoomSpeed = 0f;

            if (Math.Abs(_cameraControlModel.ZoomValue.y) >= 0.01f)
            {
                targetZoomSpeed = _cameraControlDescription.ZoomSpeed * _cameraControlModel.ZoomValue.y;
            }

            _currentZoomSpeed = Mathf.Lerp(_currentZoomSpeed, targetZoomSpeed,
                _cameraControlDescription.ZoomSmoothihg * deltaTime);

            axis.Value -= _currentZoomSpeed;
            axis.Value = Mathf.Clamp(axis.Value, axis.Range.x, axis.Range.y);

            _cameraControlView.OrbitalFollow.RadialAxis = axis;
        }

        private void UpdateOrbit(float deltaTime)
        {
            Vector2 targetOrbitSpeed;

            if (_cameraControlModel.MiddleClickValue)
            {
                var orbit = _cameraControlModel.LookValue;
                targetOrbitSpeed = orbit * _cameraControlDescription.LookSpeed;
            }
            else
            {
                targetOrbitSpeed = Vector2.zero;
            }

            _currentOrbitSpeed.x = Mathf.Lerp(_currentOrbitSpeed.x, targetOrbitSpeed.x,
                _cameraControlDescription.LookSmoothihg * deltaTime);
            _currentOrbitSpeed.y = Mathf.Lerp(_currentOrbitSpeed.y, targetOrbitSpeed.y,
                _cameraControlDescription.LookSmoothihg * deltaTime);

            var horizontalAxis = _cameraControlView.OrbitalFollow.HorizontalAxis;
            var verticalAxis = _cameraControlView.OrbitalFollow.VerticalAxis;

            horizontalAxis.Value += _currentOrbitSpeed.x;
            verticalAxis.Value -= _currentOrbitSpeed.y;

            verticalAxis.Value = Mathf.Clamp(verticalAxis.Value, verticalAxis.Range.x, verticalAxis.Range.y);

            _cameraControlView.OrbitalFollow.HorizontalAxis = horizontalAxis;
            _cameraControlView.OrbitalFollow.VerticalAxis = verticalAxis;
        }

        private void UpdateMovement(float deltaTime)
        {
            var moveValue = _cameraControlModel.MoveValue;
            var forward = _cameraControlView.CameraMainTransform.forward;
            forward.y = 0f;
            forward.Normalize();

            var right = _cameraControlView.CameraMainTransform.right;
            right.y = 0f;
            right.Normalize();

            var targetMoveSpeed = (forward * moveValue.y + right * moveValue.x) * _cameraControlDescription.MoveSpeed;

            _currentMoveSpeed = Vector3.Lerp(_currentMoveSpeed,
                moveValue.sqrMagnitude > 0.01f ? targetMoveSpeed : Vector3.zero,
                _cameraControlDescription.MoveSmoothihg * deltaTime);

            var motion = _currentMoveSpeed * deltaTime;

            _cameraControlView.Transform.position += motion;
        }
    }
}