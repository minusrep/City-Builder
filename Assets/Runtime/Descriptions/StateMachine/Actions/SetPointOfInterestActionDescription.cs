using System.Collections.Generic;

namespace Runtime.StateMachine.Descriptions
{
    public class SetPointOfInterestActionDescription : ActionDescription
    {
        private const string PointOfInterestKey = "point_of_interest";

        public string PointOfInterest { get; private set; }

        public SetPointOfInterestActionDescription(Dictionary<string, object> data) : base(data)
        {
            PointOfInterest =  data[PointOfInterestKey] as string;   
        }
    }
}