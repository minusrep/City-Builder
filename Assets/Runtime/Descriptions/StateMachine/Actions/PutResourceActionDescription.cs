using System.Collections.Generic;
using System.Linq;
using Runtime.Colony;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Citizens;
using UnityEngine;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class PutResourceActionDescription : ActionDescription
    {
        private const string PointOfInterestKey= "point_of_interest";

        private string PointOfInterest { get; }
        
        public PutResourceActionDescription(Dictionary<string, object> data) : base(data)
        {
            PointOfInterest =  data[PointOfInterestKey] as string;   
        }

        public override void Execute(World world, CitizenModel model)
        {
            model.Flags["is_carrying"] = true;
            model.Flags["has_order"] = true;
            
            var buildingPosition = model.PointsOfInterest[PointOfInterest];
            var inventoryBuilding = (IInventoryBuilding)world.Buildings.Models.Values.First(b => 
                b.WorldPosition == new Vector2(buildingPosition.x, buildingPosition.z)
            );

            var resource = model.Inventory.Models.First().Value.Resource;
            var amount = model.Inventory.Models.First().Value.Amount;
            inventoryBuilding.TryAddItem(resource, amount);
            model.Inventory.TryRemoveItem(resource, amount);
            model.Flags["is_carrying"] = false;
            model.Flags["has_order"] = false;

            if (resource.Id == "worker")
            {
                model.Flags["is_working"] = true;
                model.Inventory.Models.Values.First().TryAdd(resource, -1, 100);
            }
        }
    }
}