using System;
using System.Collections.Generic;
using Runtime.Colony.ModelCollections;

namespace Runtime.StateMachine.Descriptions
{
    public class StateDescription : IDeserializeModel
    {
        private const string TransitionKey = "transitions";
        
        public List<TransitionDescription> Transitions { get; private set; }

        public void Deserialize(Dictionary<string, object> data)
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