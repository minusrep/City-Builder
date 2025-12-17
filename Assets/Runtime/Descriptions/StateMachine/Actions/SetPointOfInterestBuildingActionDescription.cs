using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class SetPointOfInterestBuildingActionDescription : ActionDescription
    {
        private const string BuildingPointOfInterestKey = "point_of_interest";
        
        public string BuildingPointOfInterest { get; private set; }

        public SetPointOfInterestBuildingActionDescription(Dictionary<string, object> data) : base(data)
        {
            BuildingPointOfInterest =  data.GetString(BuildingPointOfInterestKey);
        }
    }
}