using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Citizens;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public abstract class ActionDescription 
    {
        private const string TypeKey = "type";
        
        public string Type { get; private set; }

        protected ActionDescription(Dictionary<string, object> data)
        {
            Type = data[TypeKey] as string;
        }

        public abstract void Execute(World world, CitizenModel model);
    }
}