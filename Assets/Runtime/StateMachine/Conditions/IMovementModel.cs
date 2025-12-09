using UnityEngine;

namespace Runtime.Core
{
    public interface IMovementModel
    {
        Vector2 PointOfInterest { get; }
        
        Vector2 Position { get; }
    }
}