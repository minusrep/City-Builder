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
        
        public string System { get; private set; }

        public RegisterSystemActionDescription(Dictionary<string, object> data) : base(data)
        {
            System = data.GetString(SystemKey);
        }

        public override void Execute(World world, CitizenModel model)
        {
            var registerSystem = world.GameSystems.Get(System) as RegisterGameSystem<CitizenModel>;
            
            registerSystem.Register(model);
        }
    }
}