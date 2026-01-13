using Runtime.Descriptions.CameraControl;
using Runtime.GameSystems;
using System;
using UnityEngine;

namespace Runtime.CameraControl
{
    public class CameraControlSystem : IGameSystem
    {
        public string Id => "camera_control";

        private float _currentZoomSpeed;
        private Vector2 _currentOrbitSpeed;
        private Vector3 _currentMoovSpeed;

        private CameraControlModel _cameraControlModel;
        private CameraControlView _cameraControlView;
        private CameraControlDescription _cameraControlDescription;

        public CameraControlSystem(CameraControlModel cameraControlModel, CameraControlView cameraControlView, CameraControlDescription cameraControlDescription)
        {
            _cameraControlModel = cameraControlModel;
            _cameraControlView = cameraControlView;
            _cameraControlDescription = cameraControlDescription;
        }

        public void Update(float deltaTime)
        {
            UpdateZoom(deltaTime);
            UpdateOrbit(deltaTime);
            UpdateMovement(deltaTime);
        }

        private void UpdateZoom(float deltaTime)
        {
            var axis = _cameraControlView.OrbitalFollow.RadialAxis;

            var targetZoomSpeed = 0f;

            if (Math.Abs(_cameraControlModel.ZoomValue.y) >= 0.01f)
            {
                targetZoomSpeed = _cameraControlDescription.ZoomSpeed * _cameraControlModel.ZoomValue.y;
            }

            _currentZoomSpeed = Mathf.Lerp(_currentZoomSpeed, targetZoomSpeed, _cameraControlDescription.ZoomSmoothihg * deltaTime);

            axis.Value -= _currentZoomSpeed;
            axis.Value = Mathf.Clamp(axis.Value, axis.Range.x, axis.Range.y);

            _cameraControlView.OrbitalFollow.RadialAxis = axis;
        }

        private void UpdateOrbit(float deltaTime)
        {
            var targetOrbitSpeed = Vector2.zero;

            if (_cameraControlModel.MiddleClickValue)
            {
                var orbit = _cameraControlModel.LookValue;
                targetOrbitSpeed = orbit * _cameraControlDescription.LookSpeed;
            }
            else
            {
                targetOrbitSpeed = Vector2.zero;
            }

            _currentOrbitSpeed.x = Mathf.Lerp(_currentOrbitSpeed.x, targetOrbitSpeed.x, _cameraControlDescription.LookSmoothihg * deltaTime);
            _currentOrbitSpeed.y = Mathf.Lerp(_currentOrbitSpeed.y, targetOrbitSpeed.y, _cameraControlDescription.LookSmoothihg * deltaTime);

            var horizontalAxis = _cameraControlView.OrbitalFollow.HorizontalAxis;
            var verticalAxis = _cameraControlView.OrbitalFollow.VerticalAxis;

            horizontalAxis.Value += _currentOrbitSpeed.x;
            verticalAxis.Value -= _currentOrbitSpeed.y;

            horizontalAxis.Value = Mathf.Clamp(horizontalAxis.Value, horizontalAxis.Range.x, horizontalAxis.Range.y);
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

            var targetMoovSpeed = (forward * moveValue.y + right * moveValue.x) * _cameraControlDescription.MoveSpeed;

            if (moveValue.sqrMagnitude > 0.01f)
            {
                _currentMoovSpeed = Vector3.Lerp(_currentMoovSpeed, targetMoovSpeed, _cameraControlDescription.MoveSmoothihg * deltaTime);
            }
            else
            {
                _currentMoovSpeed = Vector3.Lerp(_currentMoovSpeed, Vector3.zero, _cameraControlDescription.MoveSmoothihg * deltaTime);
            }

            var motion = _currentMoovSpeed * deltaTime;

            _cameraControlView.Transform.position += motion;
        }
    }
}
