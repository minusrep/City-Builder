using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Citizens;
using Runtime.Extensions;
using Runtime.GameSystems;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class RegisterSystemActionDescription : ActionDescription
    {
        private const string SystemKey = "system";

        private string System { get; }

        public RegisterSystemActionDescription(Dictionary<string, object> data)
        {
            System = data.GetString(SystemKey);
        }

        public override void Execute(World world, CitizenModel model)
        {
            var registerSystem = (RegisterGameSystem<CitizenModel>)world.GameSystems.Get(System);
            
            registerSystem.Register(model);
        }
    }
}