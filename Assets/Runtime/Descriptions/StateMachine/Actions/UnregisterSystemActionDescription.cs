using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Citizens;
using Runtime.Extensions;
using Runtime.GameSystems;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class UnregisterSystemActionDescription : ActionDescription
    {
        private const string SystemKey = "system";

        private string System { get; }

        public UnregisterSystemActionDescription(Dictionary<string, object> data)
        {
            System = data.GetString(SystemKey);
        }

        public override void Execute(World world, CitizenModel model)
        {
            var registerSystem = (RegisterGameSystem<CitizenModel>)world.GameSystems.Get(System);
            
            registerSystem.Unregister(model);
        }
    }
}