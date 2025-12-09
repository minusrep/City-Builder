using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Core;
using Runtime.ModelCollections;

namespace Runtime.StateMachine.Descriptions.Conditions
{
    public abstract class ConditionDescription
    {
        private const string TypeKey = "type";
        public string Type { get; private set; }

        protected ConditionDescription(Dictionary<string, object> data)
        {
            Type = data[TypeKey] as string;
        }
        
        public abstract bool Check(World world, IUserConditionModel user);
    }
}