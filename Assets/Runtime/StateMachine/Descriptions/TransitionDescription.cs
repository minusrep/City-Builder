using System.Collections.Generic;
using System.Linq;
using Runtime.Colony.ModelCollections;

namespace Runtime.StateMachine
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