using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Descriptions.StateMachine;
using Runtime.ModelCollections;
using Runtime.Utilities;

namespace Runtime.StateMachine
{
    public class StateMachineModel : ISerializeModel, IDeserializeModel
    {
        private const string CurrentStateKey = "current_state";
        
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

        public Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>()
            {
                { CurrentStateKey, CurrentStateName }
            };
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            var currentStateName = data.TryGetValue(CurrentStateKey, out var value) ? value.ToString() : _stateDescriptionCollection.States.Keys.First();
            
            CurrentStateName = currentStateName;
        }
    }
}