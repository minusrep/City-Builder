using System.Collections.Generic;
using Runtime.Colony.ModelCollections;

namespace Runtime.StateMachine
{
    public abstract class ConditionDescription : IDeserializeModel
    {
        private const string TypeKey = "type";
        public string Type { get; private set; }

        public abstract bool Check(Dictionary<string, object> model);
        
        public virtual void Deserialize(Dictionary<string, object> data)
        {
            Type = data[TypeKey] as string;
        }
    }
}