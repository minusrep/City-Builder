using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Colony.StateMachine.Conditions
{
    public interface IMovementModel
    {
        Dictionary<string, Vector3> PointsOfInterest { get; set; }

        Vector3 Position { get; set; }
    }
}