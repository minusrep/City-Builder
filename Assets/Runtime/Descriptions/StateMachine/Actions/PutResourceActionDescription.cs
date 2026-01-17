using System.Collections.Generic;
using System.Linq;
using Runtime.Colony;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Citizens;
using Runtime.Extensions;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class PutResourceActionDescription : ActionDescription
    {
        private const string PointOfInterestKey= "point_of_interest";

        private string PointOfInterest { get; }
        
        public PutResourceActionDescription(Dictionary<string, object> data)
        {
            PointOfInterest = data.GetString(PointOfInterestKey);
        }

        public override void Execute(World world, CitizenModel model)
        {
            model.Flags["is_carrying"] = true;
            model.Flags["has_order"] = true;
            
            var buildingPosition = model.PointsOfInterest[PointOfInterest];
            var inventoryBuilding = (IInventoryBuilding)world.Grid.GetBuilding(buildingPosition);

            var resource = model.Inventory.Models.First().Value.Resource;
            inventoryBuilding.TryAddItem(resource, 1);
            model.Inventory.TryRemoveItem(resource, 1);
            model.Flags["is_carrying"] = false;
            model.Flags["has_order"] = false;

            if (resource.Id == "worker")
            {
                model.Flags["is_working"] = true;
                model.Inventory.TryAddItem(resource, 0);
            }
        }
    }
}