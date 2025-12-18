using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.StateMachine.Conditions;
using Runtime.Extensions;

namespace Runtime.Descriptions.StateMachine.Conditions
{
    public abstract class ConditionDescription 
    {
        private const string TypeKey = "type";
        public string Type { get; private set; }

        protected ConditionDescription(Dictionary<string, object> data)
        {
            Type = data.GetString(TypeKey);
        }
        
        public abstract bool Check(World world, IUserConditionModel user);
    }
}