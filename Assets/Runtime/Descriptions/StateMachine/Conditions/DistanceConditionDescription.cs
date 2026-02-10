using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.StateMachine.Conditions;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Descriptions.StateMachine.Conditions
{
    public class DistanceConditionDescription : ConditionDescription
    {
        private const string ValueKey = "value";
        
        private const string PointOfInterestKey = "point_of_interest";
        
        private string PointOfInterest { get; }
        private float Value { get; }

        public DistanceConditionDescription(Dictionary<string, object> data) : base(data)
        {
            Value = data.GetFloat(ValueKey);

            PointOfInterest = data.GetString(PointOfInterestKey);
        }

        public override bool Check(World world, IUserConditionModel user)
        {
            if (user is not IMovementModel movementModel)
            {
                return false;
            }
            
            var to = movementModel.PointsOfInterest[PointOfInterest];

            var from = movementModel.Position;
            
            return Vector3.Distance(from, to) <= Value;
        }
    }
}