using System;
using System.Collections.Generic;

namespace Runtime.Colony.Citizens.StateMachine.Temp
{
    public class CitizenStateDescription : IDeserializeModel
    {
        private const string NameKey = "name";
        private const string TransitionsKey = "transitions";
        
        public string Name { get; private set; }
        
        public List<CitizenTransitionDescription> Transitions { get; private set; }

        public void Deserialize(Dictionary<string, object> data)
        {
            Name = data[NameKey] as string;            
            
            Transitions = new List<CitizenTransitionDescription>();
            
            if (data[TransitionsKey] is not List<object> transitionObjects)
            {
                throw new Exception("Conditions list doesn't contain conditions");
            }
            
            foreach (var transitionObject in transitionObjects)
            {
                if (transitionObject is not Dictionary<string, object> transition)
                {
                    throw new Exception("Conditions list doesn't contain conditions");
                }
                
                var transitionDescription =  new CitizenTransitionDescription();
                
                transitionDescription.Deserialize(transition);
                
                Transitions.Add(transitionDescription);
            }
        }
    }
}