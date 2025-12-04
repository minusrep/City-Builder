using System;
using System.Collections.Generic;
using System.Linq;
using Runtime.Descriptions.Citizens;

namespace Runtime.Colony.Citizens.StateMachine
{
    public class CitizenStateMachineModel : ISerializeModel, IDeserializeModel
    {
        private const string CurrentStateKey = "currentState";
        
        private const string StatesKey = "states";

        public event Action OnChangeState;

        public CitizenStateDescription CurrentStateDescription { get; private set; }

        private readonly Dictionary<string, CitizenStateDescription> _states;

        public CitizenStateMachineModel(Dictionary<string, CitizenStateDescription> stateDescriptions)
        {
            _states = stateDescriptions;
            
            CurrentStateDescription = _states.Values.First();
        }

        public void Enter(string stateName)
        {
            CurrentStateDescription = _states[stateName];
            
            OnChangeState?.Invoke();
        }

        public Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>()
            {
                {CurrentStateKey, CurrentStateDescription.Name}
            };
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            var currentState = data[CurrentStateKey] as string;
            
            CurrentStateDescription = _states[currentState];
        }
    }
}