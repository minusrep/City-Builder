using System.Collections.Generic;
using Runtime.ModelCollections;

namespace Runtime.StateMachine.Descriptions.Actions
{
    public abstract class ActionDescription :  IDeserializeModel
    {
        private const string TypeKey = "type";
        
        public string Type { get; private set; }
        
        public virtual void Deserialize(Dictionary<string, object> data)
        {
            Type = data[TypeKey] as string;
        }
    }
}