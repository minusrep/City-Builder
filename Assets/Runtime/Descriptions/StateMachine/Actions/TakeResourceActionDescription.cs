using System.Collections.Generic;
using System.Linq;
using Runtime.Colony;
using Runtime.Colony.Buildings.Production;
using Runtime.Colony.Citizens;
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
            var building = world.Buildings.Models.First(b => 
                b.Value.BaseDescription.Id == PointOfInterest &&
                b.Value.Position == new Vector2(buildingPosition.x, buildingPosition.z)
            ).Value;

            if (building is not ProductionBuildingModel productionBuilding)
            {
                return;
            }

            if (!productionBuilding.Inventory.TryRemoveItem(productionBuilding.ResourceDescription, 1))
            {
                return;
            }

            model.Inventory.TryAddItem(productionBuilding.ResourceDescription, 1);
            model.Flags["is_carrying"] = true;
        }
    }
}