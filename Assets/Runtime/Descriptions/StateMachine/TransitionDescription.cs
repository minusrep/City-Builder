using System.Collections.Generic;
using System.Linq;
using Runtime.ModelCollections;
using Runtime.StateMachine.Descriptions.Conditions;
using Runtime.StateMachine.Descriptions.Extensions;

namespace Runtime.StateMachine.Descriptions
{
    public class TransitionDescription : IDeserializeModel
    {
        public string ToState { get; private set; }
        
        public ConditionDescription Condition { get; private set; }
        
        public void Deserialize(Dictionary<string, object> data)
        {
            var first = data.First();
            
            ToState = first.Key;
            
            var conditionDictionary = first.Value as Dictionary<string, object>;
            
            Condition = conditionDictionary.ToConditionDescription();
        }
    }
}