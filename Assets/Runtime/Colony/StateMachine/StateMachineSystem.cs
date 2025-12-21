using Runtime.GameSystems;

namespace Runtime.Colony.StateMachine
{
    public class StateMachineSystem : IGameSystem
    {
        public string Id => "state_machine";
        
        private readonly World _world;
        
        public StateMachineSystem(World world)
        {
            _world = world;
        }

        public void Update(float deltaTime)
        {
            foreach (var citizen in _world.Citizens.Models.Values)
            {
                foreach (var transition in citizen.StateMachine.CurrentState.Transitions)
                {
                    if (!transition.Condition.Check(_world, citizen)) continue;
                
                    citizen.StateMachine.Enter(transition.ToState);
                    
                    break;
                }
            }
        }
    }
}