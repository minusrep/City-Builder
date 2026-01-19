using System.Linq;
using Runtime.Colony;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Production;
using Runtime.Colony.Buildings.Storage;
using Runtime.Colony.Citizens;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class TakeOrderActionDescription : ActionDescription
    {
        public override void Execute(World world, CitizenModel model)
        {
            model.Flags["has_order"] = false;
            
            var order = world.OrderManager.TakeOrder();

            var productionBuilding = (ProductionBuildingModel)world.Buildings.Get(order.FromBuildingId);

            var resource = world.WorldDescription.ResourceCollection.Descriptions[order.ResourceId];
            
            BuildingModel targetBuilding = null;
            BuildingModel sourceBuilding = null;

            if (order.Type == "put_resource")
            {
                if (order.ResourceId == "worker")
                {
                    model.Inventory.TryAddItem(resource, 1);
                    model.Flags["is_carrying"] = true;
                    model.SetPointOfInterest("resource_target", productionBuilding.GetInteractionPoint());
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
            model.SetPointOfInterest("resource_source", sourceBuilding.GetInteractionPoint());
            model.SetPointOfInterest("resource_target", targetBuilding.GetInteractionPoint());

            order.Reserve(order.Amount);
            model.Flags["has_order"] = true;
        }
    }
}