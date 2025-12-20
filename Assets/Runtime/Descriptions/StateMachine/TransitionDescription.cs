using System.Collections.Generic;
using System.Linq;
using Runtime.Descriptions.StateMachine.Conditions;
using Runtime.Descriptions.StateMachine.Extensions;

namespace Runtime.Descriptions.StateMachine
{
    public class TransitionDescription 
    {
        public string ToState { get; private set; }
        
        public ConditionDescription Condition { get; private set; }

        public TransitionDescription(string toState, Dictionary<string, object> data)
        {
            ToState = toState;
            
            Condition = data.ToConditionDescription();
        }
    }
}