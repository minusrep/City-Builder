using Runtime.Colony.Citizens.Movement;
using Runtime.Colony.StateMachine;
using Runtime.Common;
using Runtime.Services.Update;

namespace Runtime.Colony.Citizens
{
    public class CitizenPresenter : IPresenter
    {
        private CitizenMovementPresenter _citizenMovementPresenter;
        
        private readonly CitizenModel _model;
        
        private readonly World _world;
        
        private readonly CitizenView _view;
        
        public CitizenPresenter(CitizenView view,  CitizenModel model, World world)
        {
            _view =  view;
            
            _world =  world;
            
            _model = model;
        }
        
        public void Enable()
        {
            _citizenMovementPresenter = new CitizenMovementPresenter(_model, _view.CitizenMovementView);

            _citizenMovementPresenter.Enable();
        }

        public void Disable()
        {
            _citizenMovementPresenter.Disable();

            _citizenMovementPresenter = null;
        }
    }
}