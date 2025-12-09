using Runtime.Colony;
using Runtime.StateMachine.Conditions;
using UnityEngine;

namespace Runtime.StateMachine
{
    public class StateMachineSystem
    {
        private readonly StateMachineModel _stateMachine;
        
        private readonly World _world;
        
        private readonly IUserConditionModel _userModel;

        public StateMachineSystem(StateMachineModel  stateMachine, World world, IUserConditionModel userModel)
        {
            _stateMachine = stateMachine;

            _userModel =  userModel;
            
            _world = world;
        }
        
        public void Update()
        {
            foreach (var transition in _stateMachine.CurrentState.Transitions)
            {
                if (!transition.Condition.Check(_world, _userModel)) continue;
                
                _stateMachine.Enter(transition.ToState);
                    
                break;
            }
        }
    }
}