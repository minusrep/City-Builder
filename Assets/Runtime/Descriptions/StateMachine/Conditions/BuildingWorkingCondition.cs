using System;
using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Buildings.Production;
using Runtime.Colony.Citizens;
using Runtime.Colony.StateMachine.Conditions;

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
            var building = (ProductionBuildingModel)world.Grid.GetBuilding(buildingPosition);

            return building != null && building.IsActive == _isWorking;
        }
    }
}