using System;
using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Core;
using UnityEngine;

namespace Runtime.StateMachine.Descriptions.Conditions
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
            _distance = Convert.ToSingle(data[DistanceKey]);
            
            PointOfInterest = Convert.ToString(data[PointOfInterestKey]);
        }

        public override bool Check(World world, IUserConditionModel user)
        {
            if (user is not IMovementModel movementModel) return false;
            
            var to = movementModel.PointOfInterest;

            var from = movementModel.Position;
            
            return Vector2.Distance(from, to) <= _distance;
        }
    }
}