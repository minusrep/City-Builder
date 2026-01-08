using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Runtime.Colony;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Production;
using Runtime.Colony.Buildings.Storage;
using Runtime.Colony.Citizens;
using Runtime.Descriptions.Items;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class TakeOrderActionDescription : ActionDescription

    {
        public TakeOrderActionDescription(Dictionary<string, object> data) : base(data)
        {
        }

        public override void Execute(World world, CitizenModel model)
        {
            var productionBuildings = world.Buildings.Models.Values
                .Where(b => b is ProductionBuildingModel)
                .Cast<ProductionBuildingModel>();

            var productionBuilding = productionBuildings.First(p => p.HasOrder());
            var order = productionBuilding.Orders.Models.Values.First(o => o.FreeAmount > 0);

            var wareHouse = world.Buildings.Models.Values.First(b => b is StorageBuildingModel) as StorageBuildingModel;

            ResourceDescription resource = world.WorldDescription.ResourceCollection.Descriptions[order.ResourceId];
            BuildingModel targetbuilding = wareHouse;
            BuildingModel sourcebuilding = productionBuilding;

            if (order.Type == "put_resource")
            {
                if (order.Id == "worker")
                {
                    model.Inventory.TryAddItem(resource, 1);
                    model.Flags["is_carrying"] = true;
                    model.SetPointOfInterest("resource_target",
                        new Vector3(productionBuilding.Position.x, 0, productionBuilding.Position.y));
                    order.Select(1);
                    return;
                }

                targetbuilding = productionBuilding;
                sourcebuilding = wareHouse;
            }

            model.Inventory.TryAddItem(resource, 0);
            model.SetPointOfInterest("resource_source",
                new Vector3(sourcebuilding.Position.x, 0, sourcebuilding.Position.y));
            model.SetPointOfInterest("resource_target",
                new Vector3(targetbuilding.Position.x, 0, targetbuilding.Position.y));

            order.Select(1);
        }
    }
}