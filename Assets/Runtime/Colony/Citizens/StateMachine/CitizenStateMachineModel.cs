using System.Collections.Generic;
using System.Linq;
using Runtime.Descriptions.Citizens;

namespace Runtime.Colony.Citizens.StateMachine
{
    public class CitizenStateMachineModel : ISerializeModel, IDeserializeModel
    {
        private const string CurrentStateKey = "currentState";
        
        private const string StatesKey = "states";
        
        private readonly List<CitizenStateDescription> _states;

        private CitizenStateDescription _currentStateDescription;
        
        public CitizenStateMachineModel(List<CitizenStateDescription> states)
        {
            _states = states;
        }
        
        public void Update()
        {
            foreach (var transition in _currentStateDescription.Transitions)
            {
                var canTransit = true;
                
                foreach (var condition in transition.Conditions)
                {
                    if (condition.Check())
                    {
                        continue;
                    }
                    
                    canTransit = false;
                    
                    break;
                }

                if (canTransit)
                {
                    _currentStateDescription = _states.First(state => state.Name == transition.ToState);
                }
            }
        }

        public Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>()
            {
                {CurrentStateKey, _states.IndexOf(_currentStateDescription)}
            };
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            var currentStateIndex = (int) data[CurrentStateKey];
            
            _currentStateDescription = _states[currentStateIndex];
        }
    }
}