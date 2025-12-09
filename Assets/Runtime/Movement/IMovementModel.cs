using System;
using UnityEngine;

namespace Runtime.Core
{
    public interface IMovementModel
    {
        Vector3 PointOfInterest { get; set; }

        Vector3 Position { get; set; }
    }
}