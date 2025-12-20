using System.Collections.Generic;

namespace Runtime.Descriptions.StateMachine
{
    public class StateDescriptionCollection
    {
        public Dictionary<string, StateDescription> States { get; private set; }

        public StateDescriptionCollection(Dictionary<string, object> data)
        {
            States = new Dictionary<string, StateDescription>();

            foreach (var pair in data)
            {
                var dictionary = pair.Value as Dictionary<string, object>;
                
                var state = new StateDescription(dictionary);

                States.Add(pair.Key, state);
            } 
        }
        
        public StateDescription Get(string stateName)
        {
            return States[stateName];
        }
    }
}