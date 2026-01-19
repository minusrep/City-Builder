using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Citizens;
using Runtime.Extensions;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class EnterBuildingActionDescription : ActionDescription
    {
        private const string BuildingKey = "building";

        public EnterBuildingActionDescription(Dictionary<string, object> data)
        {
            data.GetString(BuildingKey);
        }

        public override void Execute(World world, CitizenModel model)
        {
        }
    }
}