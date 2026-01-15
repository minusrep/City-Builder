using Runtime.Input;
using UnityEngine;

namespace Runtime.CameraControl
{
    public class CameraControlModel
    {
        public readonly PlayerControls PlayerControls;

        public CameraControlModel(PlayerControls model)
        {
            PlayerControls = model;
        }

        public Vector2 MoveValue => PlayerControls.Player.Move.ReadValue<Vector2>();
        public Vector2 LookValue => PlayerControls.Player.Look.ReadValue<Vector2>();
        public Vector2 ZoomValue => PlayerControls.Player.Zoom.ReadValue<Vector2>();
        public bool MiddleClickValue => PlayerControls.Player.MiddleClick.IsPressed();
    }
}
