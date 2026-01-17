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

        private string BuildingPointOfInterest { get; }

        public SetPointOfInterestBuildingActionDescription(Dictionary<string, object> data)
        {
            BuildingPointOfInterest = data.GetString(BuildingPointOfInterestKey);
        }

        public override void Execute(World world, CitizenModel model)
        {
            var buildings = world.Buildings.Models.Values;

            var targetBuildings = buildings.Where(a => BuildingPointOfInterest == a.BaseDescription.Id).ToList();

            var targetBuilding = targetBuildings[0];

            var minDistance = Vector3.Distance(targetBuilding.WorldPosition, model.Position);

            foreach (var building in targetBuildings)
            {
                var distance = Vector3.Distance(model.Position, building.WorldPosition);

                if (distance < minDistance)
                {
                    targetBuilding = building;

                    minDistance = distance;
                }
            }

            model.SetPointOfInterest(BuildingPointOfInterest,
                targetBuilding.WorldPosition +
                targetBuilding.BaseDescription.InteractionPoints[0]);
        }
    }
}