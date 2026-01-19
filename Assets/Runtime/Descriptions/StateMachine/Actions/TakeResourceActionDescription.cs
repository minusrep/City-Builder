using System.Collections.Generic;
using System.Linq;
using Runtime.Colony;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Citizens;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class TakeResourceActionDescription : ActionDescription
    {
        private const string PointOfInterestKey = "point_of_interest";

        private string PointOfInterest { get; }

        public TakeResourceActionDescription(Dictionary<string, object> data)
        {
            PointOfInterest = data[PointOfInterestKey] as string;
        }

        public override void Execute(World world, CitizenModel model)
        {
            model.Flags["is_carrying"] = false;

            if (!model.PointsOfInterest.ContainsKey(PointOfInterest))
            {
                return;
            }

            var buildingPosition = model.PointsOfInterest[PointOfInterest];
            var inventoryBuilding = (IInventoryBuilding)world.Grid.GetBuilding(buildingPosition);

            if (model.Inventory.Models.Values.Count == 0)
            {
                return;
            }

            var resource = model.Inventory.Models.Values.First().Resource;

            if (resource == null)
            {
                return;
            }

            var amount = model.Inventory.Models.Values.First().Amount * -1;
            if (!inventoryBuilding.TryRemoveItem(resource, amount))
            {
                RestoreOrder(world, model);
                return;
            }
            
            model.Inventory.TryAddItem(resource, amount * 2);
            model.Flags["is_carrying"] = true;

            if (resource.Id == "worker")
            {
                model.Inventory.TryRemoveItem(resource, 1);
                model.Flags["is_working"] = false;
                model.Flags["is_carrying"] = false;
            }
        }

        private void RestoreOrder(World world, CitizenModel model)
        {
            if (!model.PointsOfInterest.ContainsKey("resource_target"))
            {
                return;
            }

            var buildingPosition = model.PointsOfInterest["resource_target"];
            var targetBuilding = world.Grid.GetBuilding(buildingPosition);
            
            if (model.Inventory.Models.Values.Count == 0)
            {
                return;
            }

            var resource = model.Inventory.Models.Values.First().Resource;

            if (resource == null)
            {
                return;
            }

            var order = world.OrderManager[$"{targetBuilding.Id}_{resource.Id}"];
            var amount = model.Inventory.Models.Values.First().Amount * -1;
            order.Unreserve(amount);
            model.Inventory.TryAddItem(resource, 2 * amount);
            model.Inventory.TryRemoveItem(resource, amount);
        }
    }
}