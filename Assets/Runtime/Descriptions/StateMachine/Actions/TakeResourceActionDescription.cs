using System.Collections.Generic;
using System.Linq;
using Runtime.Colony;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Citizens;
using UnityEngine;

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
            var inventoryBuildingPair = world.Buildings.Models.FirstOrDefault(b =>
                b.Value.WorldPosition == new Vector2(buildingPosition.x, buildingPosition.z)
            );

            if (inventoryBuildingPair.Value is not IInventoryBuilding inventoryBuilding)
            {
                return;
            }

            if (model.Inventory.Models.Values.Count == 0)
            {
                return;
            }

            var resource = model.Inventory.Models.Values.First().Resource;

            if (resource == null)
            {
                return;
            }

            if (!inventoryBuilding.TryRemoveItem(resource, 1))
            {
                RestoreOrder(world, model);
                return;
            }

            model.Inventory.TryAddItem(resource, 1);
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
            var (_, targetBuilding) = world.Buildings.Models.FirstOrDefault(b =>
                b.Value.WorldPosition == new Vector2(buildingPosition.x, buildingPosition.z)
            );

            if (targetBuilding == null)
            {
                return;
            }

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
            order.Unreserve(1);
            model.Inventory.Models.Values.First().TryReduce(0);
        }
    }
}