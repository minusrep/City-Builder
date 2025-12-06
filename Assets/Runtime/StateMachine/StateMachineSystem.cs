using UnityEngine;

namespace Runtime.StateMachine
{
    public class StateMachineSystem
    {
        private readonly StateMachineModel _stateMachine;
        
        private readonly ISystemModel _systemModel;

        public StateMachineSystem(StateMachineModel  stateMachine, ISystemModel systemModel)
        {
            _stateMachine = stateMachine;

            _systemModel =  systemModel;
        }
        
        public void Update()
        {
            foreach (var transition in _stateMachine.CurrentState.Transitions)
            {
                Debug.Log($"{_stateMachine.CurrentStateName}");
                Debug.Log($"{transition.Condition.Type}: {transition.Condition.Check(_systemModel.Stats)}");
                
                if (!transition.Condition.Check(_systemModel.Stats)) continue;
                
                _stateMachine.Enter(transition.ToState);
                    
                break;
            }
        }
    }
}