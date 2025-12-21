using System.Collections.Generic;
using System.Linq;
using Runtime.Colony;
using Runtime.Colony.Citizens;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class SetPointOfInterestBuildingActionDescription : ActionDescription
    {
        private const string BuildingPointOfInterestKey = "point_of_interest";
        
        public string BuildingPointOfInterest { get; private set; }

        public SetPointOfInterestBuildingActionDescription(Dictionary<string, object> data) : base(data)
        {
            BuildingPointOfInterest =  data.GetString(BuildingPointOfInterestKey);
        }

        public override void Execute(World world, CitizenModel model)
        {
            var buildings = world.Buildings.Models.Values;

            var targetBuildings = buildings.Where(a => BuildingPointOfInterest == a.BaseDescription.Id).ToList();

            var buildingPosition = targetBuildings[0].Position;
                
            var minDistance = Vector3.Distance(buildingPosition, model.Position);
                
            foreach (var building in targetBuildings)
            {
                var distance = Vector3.Distance(model.Position, building.Position);
                    
                if (distance < minDistance)
                {
                    buildingPosition = building.Position;
                        
                    minDistance = distance;
                }
            }
                
            model.SetPointOfInterest(BuildingPointOfInterest, new Vector3(buildingPosition.x, 0, buildingPosition.y));
        }
    }
}