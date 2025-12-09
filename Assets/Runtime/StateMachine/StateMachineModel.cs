using System;
using System.Linq;
using Runtime.Descriptions.StateMachine;

namespace Runtime.StateMachine
{
    public class StateMachineModel
    {
        public event Action OnChange;

        public StateDescription CurrentState => _stateDescriptionCollection.Get(CurrentStateName);

        public string CurrentStateName { get; private set; }
        
        private readonly StateDescriptionCollection _stateDescriptionCollection;
        
        public StateMachineModel(StateDescriptionCollection stateDescriptionCollection)
        {
            _stateDescriptionCollection = stateDescriptionCollection;

            var initialState = _stateDescriptionCollection.States.Keys.First();
            
            Enter(initialState);
        }

        public void Enter(string stateName)
        {
            CurrentStateName = stateName;
            
            OnChange?.Invoke();
        }
    }
}