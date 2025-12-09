using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.StateMachine.Descriptions.Conditions
{
    public class DistanceConditionDescription : ConditionDescription
    {
        private const string DistanceKey = "distance";
        
        private const string PointOfInterestKey = "point_of_interest";
        
        private const string PositionKey = "position";

        public DistanceConditionDescription(Dictionary<string, object> data) : base(data)
        {
            _distance = Convert.ToSingle(data[DistanceKey]);
            
            PointOfInterest = Convert.ToString(data[PointOfInterestKey]);
        }

        public string PointOfInterest { get; private set; }

        private float _distance;

        public override bool Check(Dictionary<string, object> model)
        {
            var to = model[PointOfInterestKey] as Vector2? ?? default;

            var from = model[PositionKey] as Vector2? ?? default;
            
            return Vector2.Distance(from, to) <= _distance;
        }
    }
}