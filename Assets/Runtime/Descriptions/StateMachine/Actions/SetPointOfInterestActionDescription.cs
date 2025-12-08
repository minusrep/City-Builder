using System.Collections.Generic;

namespace Runtime.StateMachine.Descriptions
{
    public class SetPointOfInterestActionDescription : ActionDescription
    {
        private const string PointOfInterestKey = "point_of_interest";
        
        public string PointOfInterest { get; private set; }
        
        public override void Deserialize(Dictionary<string, object> data)
        {
            base.Deserialize(data);
            
            PointOfInterest =  data[PointOfInterestKey] as string;   
        }
    }
}