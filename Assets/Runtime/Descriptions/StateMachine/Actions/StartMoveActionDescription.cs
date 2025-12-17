using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class StartMoveActionDescription : ActionDescription
    {
        private const string PointOfInterestKey= "point_of_interest";
        
        public string PointOfInterest { get; private set; }
        
        public StartMoveActionDescription(Dictionary<string, object> data) : base(data)
        {
            PointOfInterest = data.GetString(PointOfInterestKey);
        }
    }
}