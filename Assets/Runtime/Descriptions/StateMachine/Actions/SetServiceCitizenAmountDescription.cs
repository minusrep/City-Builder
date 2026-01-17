using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Buildings.Service;
using Runtime.Colony.Citizens;
using Runtime.Extensions;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class SetServiceCitizenAmountDescription : ActionDescription
    {
        private const string PointOfInterestKey = "point_of_interest";
        private const string IncrementKey = "increment";

        private string PointOfInterest { get; }
        private bool IsIncrement { get; }

        public SetServiceCitizenAmountDescription(Dictionary<string, object> data)
        {
            PointOfInterest = data.GetString(PointOfInterestKey);
            IsIncrement = data.GetBool(IncrementKey);
        }

        public override void Execute(World world, CitizenModel model)
        {
            var pointOfInterest = model.PointsOfInterest[PointOfInterest];
            
            if (world.Grid.GetBuilding(pointOfInterest) is ServiceBuildingModel targetBuilding)
            {
                if (IsIncrement)
                    targetBuilding.CurrentCitizenAmount++;
                else
                    targetBuilding.CurrentCitizenAmount--;
            }
        }
    }
}