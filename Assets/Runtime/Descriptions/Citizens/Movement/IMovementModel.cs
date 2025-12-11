using System;
using UnityEngine;

namespace Runtime.Movement
{
    public interface IMovementModel
    {
        public event Action OnChangePointOfInterest;
        
        Vector3 PointOfInterest { get; set; }

        Vector3 Position { get; set; }
    }
}