using System;
using System.Collections.Generic;
using Runtime.Colony.ModelCollections;

namespace Runtime.StateMachine.Descriptions
{
    public class StateDescription : IDeserializeModel
    {
        private const string TransitionKey = "transitions";
        
        private const string ActionsKey = "actions";

        public List<TransitionDescription> Transitions { get; private set; }

        public List<ActionDescription> Actions { get; private set; }

        public void Deserialize(Dictionary<string, object> data)
        {
            DeserializeTransitions(data);

            DeserializeActions(data);
        }

        private void DeserializeActions(Dictionary<string, object> data)
        {
            Actions = new List<ActionDescription>();

            if (data[ActionsKey] is not List<object> actionObjects) throw new Exception();

            foreach (var actionObject in actionObjects)
            {
                var actionDictionary = actionObject as Dictionary<string, object>;

                var action = actionDictionary.ToActionDescription();
                
                Actions.Add(action);
            }
        }

        private void DeserializeTransitions(Dictionary<string, object> data)
        {
            Transitions = new List<TransitionDescription>();
            
            if (data[TransitionKey] is not List<object> transitionList) throw new Exception();
            
            foreach (var transitionObject in transitionList)
            {
                var transitionDictionary = transitionObject as Dictionary<string, object>;
                
                var transition = new TransitionDescription();
                
                transition.Deserialize(transitionDictionary);
                
                Transitions.Add(transition);
            }
        }
    }
}