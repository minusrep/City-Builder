using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Descriptions.Citizens;

namespace Runtime.Colony.Citizens.StateMachine
{
    [Serializable]
    public class CitizenStateMachine 
    {
        private CitizenStateDescription _initialState;

        private TransitionDescription[] _globalTransitions;

        private Dictionary<string, CitizenState> _states;

        private CitizenState _currentState;
        
        public CitizenStateMachine(Dictionary<string, CitizenStateDescription> stateDescriptions)
        {
            _states = new Dictionary<string, CitizenState>();

            foreach (var description in stateDescriptions)
            {
                var newState = new CitizenState(description.Value);
                
                _states.Add(description.Key, newState);                
            }
            
            _currentState = _states.Values.First();
        }

        public void Update()
        {
            foreach (var transition in _currentState.Description.TransitionDescriptions)
            {
                foreach (var condition in transition.ConditionDescriptions)
                {
                    EnterState(transition.ToState);
                }
            }
        }

        private void EnterState(string state)
        {
            _currentState = _states[state];
        }
    }
}