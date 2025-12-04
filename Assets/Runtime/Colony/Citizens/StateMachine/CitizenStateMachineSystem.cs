using System.Runtime.Serialization;

namespace Runtime.Colony.Citizens.StateMachine.Temp
{
    public class CitizenStateMachineSystem
    {
        private CitizenStateMachineModel _stateMachine;

        public CitizenStateMachineSystem(CitizenStateMachineModel stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public void Enable()
        {
        }
        
        public void Disable()
        {
            
        }
    }
}