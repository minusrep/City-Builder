using Runtime.Colony.Citizens.Movement;
using Runtime.Colony.StateMachine;
using Runtime.Common;
using Runtime.Descriptions;
using Runtime.Services.Update;

namespace Runtime.Colony.Citizens
{
    public class CitizenPresenter : IPresenter
    {
        private MovementPresenter _movementPresenter;

        private StateMachinePresenter _stateMachinePresenter;
        
        private readonly CitizenModel _model;
        
        private readonly CitizenView _view;
        
        private readonly UpdateService _updateService;

        private readonly WorldDescription _worldDescription;

        public CitizenPresenter(CitizenView view,  CitizenModel model, UpdateService updateService, WorldDescription worldDescription)
        {
            _view =  view;
            
            _model = model;
            
            _updateService = updateService;
            
            _worldDescription = worldDescription;
        }
        
        public void Enable()
        {
            _movementPresenter = new MovementPresenter(_model, _view.MovementView, _updateService);

            _stateMachinePresenter = new StateMachinePresenter(_model.StateMachine, _model, _worldDescription);
            
            _movementPresenter.Enable();
        }

        public void Disable()
        {
            _movementPresenter.Disable();

            _stateMachinePresenter.Disable();
            
            _movementPresenter = null;
            
            _stateMachinePresenter = null;
        }
    }
}