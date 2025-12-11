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
        private const string DistanceKey = "distance";
        
        private const string PointOfInterestKey = "point_of_interest";
        
        private const string PositionKey = "position";
        public string PointOfInterest { get; private set; }

        private readonly float _distance;

        public DistanceConditionDescription(Dictionary<string, object> data) : base(data)
        {
            _distance = data.GetFloat(DistanceKey);

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
            
            return Vector3.Distance(from, to) <= _distance;
        }
    }
}