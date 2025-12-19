using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Citizens;
using Runtime.Extensions;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class EnterBuildingActionDescription : ActionDescription
    {
        private const string BuildingKey = "building";
        
        public string Building { get; private set; }
        
        public EnterBuildingActionDescription(Dictionary<string, object> data) : base(data)
        {
            Building = data.GetString(BuildingKey);
        }

        public override void Execute(World world, CitizenModel model)
        {

        }
    }
}