using System.Collections.Generic;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class SetPointOfInterestDescription : ActionDescription
    {
        private const string PointOfInterestKey = "point_of_interest";

        public string PointOfInterest { get; private set; }

        public SetPointOfInterestDescription(Dictionary<string, object> data) : base(data)
        {
            PointOfInterest =  data[PointOfInterestKey] as string;   
        }
    }
}