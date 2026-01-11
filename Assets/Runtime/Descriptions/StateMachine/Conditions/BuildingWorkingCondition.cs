using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Colony;
using Runtime.Colony.Buildings.Production;
using Runtime.Colony.Citizens;
using Runtime.Colony.StateMachine.Conditions;
using UnityEngine;

namespace Runtime.Descriptions.StateMachine.Conditions
{
    public class BuildingWorkingCondition: ConditionDescription
    {
        private readonly bool _isWorking;
        
        public BuildingWorkingCondition(Dictionary<string, object> data) : base(data)
        {
            _isWorking = Convert.ToBoolean(data["value"]);
        }

        public override bool Check(World world, IUserConditionModel user)
        {
            var model = (CitizenModel)user;
            
            var buildingPosition = model.PointsOfInterest["resource_target"];
            var building = world.Buildings.Models.Values.First(b => 
                b.WorldPosition == new Vector2(buildingPosition.x, buildingPosition.z)
            ) as ProductionBuildingModel;

            return building != null && building.IsActive == _isWorking;
        }
    }
}