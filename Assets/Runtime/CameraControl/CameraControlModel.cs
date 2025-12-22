using Runtime.Input;
using UnityEngine;

namespace Runtime.CameraControl
{
    public class CameraControlModel
    {
        public readonly PlayerControls playerControls;

        public CameraControlModel(PlayerControls model)
        {
            playerControls = model;
        }

        public Vector2 MoveValue => playerControls.Camera.Move.ReadValue<Vector2>();
        public Vector2 LookValue => playerControls.Camera.Look.ReadValue<Vector2>();
        public Vector2 ZoomValue => playerControls.Camera.Zoom.ReadValue<Vector2>();
        public bool MiddleClickValue => playerControls.Camera.MiddleClick.IsPressed();
    }
}
