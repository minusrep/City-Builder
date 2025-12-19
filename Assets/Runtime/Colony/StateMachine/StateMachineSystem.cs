using Runtime.Colony.Citizens;
using Runtime.GameSystems;

namespace Runtime.Colony.StateMachine
{
    public class StateMachineSystem : IGameSystem
    {
        public string Id => "state_machine";
        
        private readonly StateMachineModel _stateMachine;
        
        private readonly World _world;
        
        private readonly CitizenModel _citizenModel;

        public StateMachineSystem(StateMachineModel stateMachine, World world, CitizenModel citizenModel)
        {
            _stateMachine = stateMachine;

            _citizenModel =  citizenModel;
            
            _world = world;
        }

        public void Update(float deltaTime)
        {
            foreach (var transition in _stateMachine.CurrentState.Transitions)
            {
                if (!transition.Condition.Check(_world, _citizenModel)) continue;
                
                _stateMachine.Enter(transition.ToState);
                    
                break;
            }
        }
    }
}