using System.Collections.Generic;
using System.Linq;
using Runtime.Colony;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Citizens;
using Runtime.Colony.Orders;
using Runtime.Descriptions.Items;
using UnityEngine;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class TakeResourceActionDescription : ActionDescription
    {
        private const string PointOfInterestKey= "point_of_interest";
        
        public string PointOfInterest { get; private set; }
        
        public TakeResourceActionDescription(Dictionary<string, object> data) : base(data)
        {
            PointOfInterest =  data[PointOfInterestKey] as string;   
        }

        public override void Execute(World world, CitizenModel model)
        {
            model.Flags["is_carrying"] = false;
            
            var buildingPosition = model.PointsOfInterest[PointOfInterest];
            var inventoryBuilding = world.Buildings.Models.First(b => 
                b.Value.WorldPosition == new Vector2(buildingPosition.x, buildingPosition.z)
            ).Value as IInventoryBuilding ;

            ResourceDescription resource = model.Inventory.Models.Values.First().Resource; 
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
            var buildingPosition = model.PointsOfInterest["resource_target"];
            var targetBuilding = world.Buildings.Models.First(b => 
                b.Value.WorldPosition == new Vector2(buildingPosition.x, buildingPosition.z)
            ).Value;
            
            ResourceDescription resource = model.Inventory.Models.Values.First().Resource; 
            var order = new OrderModel(resource.Id, targetBuilding.Id)
            {
                Type = "put_resource",
                ResourceId = resource.Id,
                Amount = 1
            };
            world.OrderManager.RestoreOrder(order);
            model.Inventory.Models.Values.First().TryReduce(0);
        }
    }
}