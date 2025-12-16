using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Citizens.Movement;
using Runtime.Colony.StateMachine.Conditions;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Descriptions.StateMachine.Conditions
{
    public class DistanceConditionDescription : ConditionDescription
    {
        private const string ValueKey = "value";
        
        private const string PointOfInterestKey = "point_of_interest";
        public string PointOfInterest { get; private set; }

        private readonly float _value;

        public DistanceConditionDescription(Dictionary<string, object> data) : base(data)
        {
            _value = data.GetFloat(ValueKey);

            PointOfInterest = data.GetString(PointOfInterestKey);
        }

        public override bool Check(World world, IUserConditionModel user)
        {
            if (user is not IMovementModel movementModel)
            {
                return false;
            }
            
            var to = movementModel.PointOfInterest;

            var from = movementModel.Position;
            
            return Vector3.Distance(from, to) <= _value;
        }
    }
}