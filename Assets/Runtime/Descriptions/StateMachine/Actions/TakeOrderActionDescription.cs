using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Runtime.Colony;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Production;
using Runtime.Colony.Buildings.Storage;
using Runtime.Colony.Citizens;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class TakeOrderActionDescription : ActionDescription

    {
        public TakeOrderActionDescription(Dictionary<string, object> data) : base(data)
        {
        }

        public override void Execute(World world, CitizenModel model)
        {
            model.Flags["has_order"] = false;
            
            var order = world.OrderManager.TakeOrder();

            var productionBuilding = world.Buildings.Get(order.FromBuildingId) as ProductionBuildingModel;
            BuildingModel targetBuilding = null;
            BuildingModel sourceBuilding = null;
            
            var resource = world.WorldDescription.ResourceCollection.Descriptions[order.ResourceId];
            if (order.Type == "put_resource")
            {
                if (order.ResourceId == "worker")
                {
                    model.Inventory.TryAddItem(resource, 1);
                    model.Flags["is_carrying"] = true;
                    model.SetPointOfInterest("resource_target",
                        new Vector3(productionBuilding.WorldPosition.x, 0, productionBuilding.WorldPosition.y));
                    order.Reserve(1);
                    return;
                }

                targetBuilding = productionBuilding;
                sourceBuilding = world.Buildings.Models.Values.FirstOrDefault(b => b is StorageBuildingModel storage
                    && storage.Inventory.CanExtract(resource, order.Amount, out _));

                if (sourceBuilding == null)
                {
                    world.OrderManager.ToBack(order.Id);
                    return;
                }
            }
            else if (order.Type == "take_resource")
            {
                sourceBuilding = productionBuilding;
                targetBuilding = world.Buildings.Models.Values.FirstOrDefault(b => b is StorageBuildingModel storage
                    && storage.Inventory.CanFit(resource, order.Amount, out _));

                if (targetBuilding == null)
                {
                    world.OrderManager.ToBack(order.Id);
                    return;
                }
            }

            model.Inventory.TryAddItem(resource, order.Amount * -1);
            model.SetPointOfInterest("resource_source",
                new Vector3(sourceBuilding.WorldPosition.x, 0, sourceBuilding.WorldPosition.y));
            model.SetPointOfInterest("resource_target",
                new Vector3(targetBuilding.WorldPosition.x, 0, targetBuilding.WorldPosition.y));

            order.Reserve(order.Amount);
            model.Flags["has_order"] = true;
        }
    }
}