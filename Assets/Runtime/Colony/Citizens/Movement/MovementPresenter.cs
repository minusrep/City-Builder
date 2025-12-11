using Runtime.Colony.StateMachine;
using Runtime.Common;
using Runtime.Descriptions;
using Runtime.Services.Update;

namespace Runtime.Colony.Citizens.Movement
{
    public class MovementPresenter : IPresenter
    {
        private readonly IMovementModel _model;
        
        private readonly StateMachineModel _stateMachineModel;

        private readonly MovementView _view;
        
        private readonly PointOfInterestDescriptionCollection _points;
        
        private readonly UpdateService _updateService;

        public MovementPresenter(IMovementModel model, MovementView view, UpdateService updateService)
        {
            _updateService = updateService;

            _view = view;

            _model = model;
        }

        public void Enable()
        {
            _updateService.OnUpdate += Update;

            _model.OnChangePointOfInterest += OnChangePointOfInterest;
            
            _view.NavMeshAgent.isStopped = false;
        }

        public void Disable()
        {
            _updateService.OnUpdate -= Update;
            
            _stateMachineModel.OnChange -= OnChangePointOfInterest;
            
            _view.NavMeshAgent.isStopped = true;
        }

        private void Update()
        {
            _model.Position = _view.Transform.position;
        }

        private void OnChangePointOfInterest()
        {
            _view.NavMeshAgent.SetDestination(_model.PointOfInterest);
        }
    }
}