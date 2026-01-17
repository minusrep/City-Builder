using Runtime.Colony.Citizens;
using Runtime.GameSystems;

namespace Runtime.Colony.StateMachine
{
    public class StateMachineSystem : RegisterGameSystem<CitizenModel>
    {
        private readonly World _world;

        public StateMachineSystem(World world) : base("state_machine")
        {
            _world = world;
        }

        protected override void Update(CitizenModel item, float deltaTime)
        {
            foreach (var transition in item.StateMachine.CurrentState.Transitions)
            {
                if (!transition.Condition.Check(_world, item))
                {
                    continue;
                }
                    
                item.StateMachine.Enter(transition.ToState);
                    
                break;
            }     
        }
    }
}