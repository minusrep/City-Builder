using System.Collections.Generic;

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
    }
}