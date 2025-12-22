using System.Collections.Generic;
using System.Linq;
using Runtime.Colony;
using Runtime.Colony.Buildings.Storage;
using Runtime.Colony.Citizens;
using UnityEngine;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class PutResourceActionDescription : ActionDescription
    {
        private const string PointOfInterestKey= "point_of_interest";
        
        public string PointOfInterest { get; private set; }
        
        public PutResourceActionDescription(Dictionary<string, object> data) : base(data)
        {
            PointOfInterest =  data[PointOfInterestKey] as string;   
        }

        public override void Execute(World world, CitizenModel model)
        {
            model.Flags["is_carrying"] = true;
            
            var buildingPosition = model.PointsOfInterest[PointOfInterest];
            var building = world.Buildings.Models.First(b => 
                b.Value.BaseDescription.Id == PointOfInterest &&
                b.Value.Position == new Vector2(buildingPosition.x, buildingPosition.z)
            ).Value;

            if (building is not StorageBuildingModel storageBuilding)
            {
                return;
            }

            var resource = model.Inventory.Models.First().Value.Resource;
            if (!storageBuilding.Inventory.TryAddItem(resource, 1))
            {
                return;
            }

            model.Inventory.TryRemoveItem(resource, 1);
            model.Flags["is_carrying"] = false;
        }
    }
}