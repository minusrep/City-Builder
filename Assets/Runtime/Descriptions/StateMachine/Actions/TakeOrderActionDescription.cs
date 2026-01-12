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
            var order = world.OrderManager.TakeOrder();
            
            var productionBuilding = world.Buildings.Get(order.FromBuildingId) as ProductionBuildingModel;
            var wareHouse = world.Buildings.Models.Values.First(b => b is StorageBuildingModel) as StorageBuildingModel;

            var resource = world.WorldDescription.ResourceCollection.Descriptions[order.ResourceId];
            BuildingModel targetbuilding = wareHouse;
            BuildingModel sourcebuilding = productionBuilding;

            if (order.Type == "put_resource")
            {
                if (order.ResourceId == "worker")
                {
                    model.Inventory.TryAddItem(resource, 1);
                    model.Flags["is_carrying"] = true;
                    model.SetPointOfInterest("resource_target",
                        new Vector3(productionBuilding.WorldPosition.x, 0, productionBuilding.WorldPosition.y));
                    order.Amount -= 1;
                    return;
                }

                targetbuilding = productionBuilding;
                sourcebuilding = wareHouse;
            }

            model.Inventory.TryAddItem(resource, 0);
            model.SetPointOfInterest("resource_source",
                new Vector3(sourcebuilding.WorldPosition.x, 0, sourcebuilding.WorldPosition.y));
            model.SetPointOfInterest("resource_target",
                new Vector3(targetbuilding.WorldPosition.x, 0, targetbuilding.WorldPosition.y));

            order.Amount -= 1;
        }
    }
}