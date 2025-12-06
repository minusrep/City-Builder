using System.Collections.Generic;
using Runtime.Colony.ModelCollections;

namespace Runtime.StateMachine
{
    public class StateDescriptionCollection : IDeserializeModel
    {
        public Dictionary<string, StateDescription> States { get; private set; }

        public StateDescription Get(string stateName)
        {
            return States[stateName];
        }
        
        public void Deserialize(Dictionary<string, object> data)
        {
            States = new Dictionary<string, StateDescription>();

            foreach (var pair in data)
            {
                var state = new StateDescription();
                
                var dictionary = pair.Value as Dictionary<string, object>;
                
                state.Deserialize(dictionary);
                
                States.Add(pair.Key, state);
            } 
        }
    }
}