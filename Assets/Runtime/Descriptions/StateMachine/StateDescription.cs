using System.Collections.Generic;
using Runtime.Descriptions.StateMachine.Actions;
using Runtime.Descriptions.StateMachine.Extensions;
using Runtime.Extensions;

namespace Runtime.Descriptions.StateMachine
{
    public class StateDescription
    {
        private const string TransitionKey = "transitions";
        
        private const string ActionsKey = "actions";

        public List<TransitionDescription> Transitions { get; private set; }

        public List<ActionDescription> Actions { get; private set; }

        public StateDescription(Dictionary<string, object> data)
        {
            Actions = new List<ActionDescription>();
            Transitions = new List<TransitionDescription>();

            var actionList = data[ActionsKey] as List<object>;
            
            foreach (var action in actionList)
            {
                var actionDictionary = action as Dictionary<string, object>;
                
                Actions.Add(actionDictionary.ToActionDescription());
            }

            var transitions = data.GetNode(TransitionKey);

            foreach (var transition in transitions)
            {
                Transitions.Add(new TransitionDescription(transition.Key, transition.Value as Dictionary<string, object>));
            }
        }
    }
}