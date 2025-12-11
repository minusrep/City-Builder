using Runtime.Colony.Citizens;
using Runtime.Common;
using Runtime.Descriptions.Citizens.Movement;
using Runtime.StateMachine;

namespace Runtime.Descriptions.Citizens
{
    public class CitizenPresenter : IPresenter
    {
        private MovementPresenter _movementPresenter;

        private readonly CitizenModel _model;
        
        private readonly CitizenView _view;
        
        private readonly UpdateService _updateService;

        public CitizenPresenter(CitizenView view,  CitizenModel model, UpdateService updateService)
        {
            _view =  view;
            
            _model = model;
            
            _updateService = updateService;
        }
        
        public void Enable()
        {
            _movementPresenter = new MovementPresenter(_model, _view.MovementView, _updateService);
            
            _movementPresenter.Enable();
        }

        public void Disable()
        {
            _movementPresenter.Disable();

            _movementPresenter = null;
        }
    }
}