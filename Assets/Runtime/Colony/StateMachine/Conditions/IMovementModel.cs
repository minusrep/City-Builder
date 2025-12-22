using System.Collections.Generic;
using Runtime.Colony.Citizens.Collection;
using UnityEngine;

namespace Runtime.Colony.StateMachine.Conditions
{
    public interface IMovementModel
    {
        PointOfInterestCollection PointsOfInterest { get; set; }

        Vector3 Position { get; set; }
    }
}