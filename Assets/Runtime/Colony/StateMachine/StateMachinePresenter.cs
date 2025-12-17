using Runtime.Colony.Citizens;
using Runtime.Common;

namespace Runtime.Colony.StateMachine
{
    public class StateMachinePresenter : IPresenter
    {
        private readonly CitizenModel _model;
        
        private readonly World _world;

        public StateMachinePresenter(CitizenModel model, World world)
        {
            _model =  model;

            _world = world;
        }
        
        public void Enable()
        {
        }

        public void Disable()
        {
        }
    }
}