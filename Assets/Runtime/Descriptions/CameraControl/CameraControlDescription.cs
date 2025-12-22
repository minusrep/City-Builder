using Runtime.Extensions;
using System.Collections.Generic;

namespace Runtime.Descriptions.CameraControl
{
    public class CameraControlDescription
    {
        public float MoveSpeed { get; }
        public float MoveSmoothihg { get; }
        public float ZoomSpeed { get; }
        public float ZoomSmoothihg { get; }
        public float LookSpeed { get; }
        public float LookSmoothihg { get; }


        public CameraControlDescription(Dictionary<string, object> data)
        {
            MoveSpeed = data.GetFloat("move_speed");
            MoveSmoothihg = data.GetFloat("move_smoothihg");
            ZoomSpeed = data.GetFloat("zoom_speed");
            ZoomSmoothihg = data.GetFloat("zoom_smoothihg");
            LookSpeed = data.GetFloat("look_speed");
            LookSmoothihg = data.GetFloat("look_smoothing");
        }
    }
}
