using UnityEngine;

namespace Runtime.Movement
{
    public interface IMovementModel
    {
        Vector3 PointOfInterest { get; set; }

        Vector3 Position { get; set; }
    }
}