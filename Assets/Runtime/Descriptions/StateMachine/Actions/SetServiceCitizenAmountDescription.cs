using System.Collections.Generic;
using System.Linq;
using Runtime.Colony;
using Runtime.Colony.Buildings.Service;
using Runtime.Colony.Citizens;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class SetServiceCitizenAmountDescription : ActionDescription
    {
        private const string PointOfInterestKey = "point_of_interest";
        private const string IncrementKey = "increment";

        private string PointOfInterest { get; }
        private bool IsIncrement { get; }

        public SetServiceCitizenAmountDescription(Dictionary<string, object> data) : base(data)
        {
            PointOfInterest = data.GetString(PointOfInterestKey);
            IsIncrement = data.GetBool(IncrementKey);
        }

        public override void Execute(World world, CitizenModel model)
        {
            var buildings = world.Buildings.Models.Values;
            var pointOfInterest = model.PointsOfInterest[PointOfInterest];
            Debug.Log($"Point of interest: {PointOfInterest}, Position: {pointOfInterest}");
            
            if (buildings.First(a => PointOfInterest == a.BaseDescription.Id && a.WorldPosition == new Vector2(pointOfInterest.x, pointOfInterest.z)) is ServiceBuildingModel targetBuilding)
            {
                if (IsIncrement)
                    targetBuilding.CurrentCitizenAmount++;
                else
                    targetBuilding.CurrentCitizenAmount--;
            }
        }
    }
}