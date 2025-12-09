using Runtime.Core;
using Runtime.Descriptions;
using Runtime.Descriptions.StateMachine.Actions;
using Runtime.StateMachine;

namespace Runtime.Movement
{
    public class MovementPresenter : IPresenter
    {
        private readonly IMovementModel _model;
        
        private readonly StateMachineModel _stateMachineModel;

        private readonly MovementView _view;
        
        private readonly PointOfInterestDescriptionCollection _points;
        
        private readonly UpdateService _updateService;

        public MovementPresenter(IMovementModel model, MovementView view, UpdateService updateService, StateMachineModel stateMachineModel, PointOfInterestDescriptionCollection points)
        {
            _updateService = updateService;
            
            _view = view;
            
            _model = model;
            
            _stateMachineModel =  stateMachineModel;

            _points = points;
        }

        public void Enable()
        {
            _updateService.OnUpdate += Update;
            
            _stateMachineModel.OnChange += OnChangeState;
            
            _view.NavMeshAgent.isStopped = false;
        }

        public void Disable()
        {
            _updateService.OnUpdate -= Update;
            
            _stateMachineModel.OnChange -= OnChangeState;
            
            _view.NavMeshAgent.isStopped = true;
        }

        private void Update()
        {
            _model.Position = _view.Transform.position;
        }

        private void OnChangeState()
        {
            foreach (var action in _stateMachineModel.CurrentState.Actions)
            {
                if (action is not SetPointOfInterestActionDescription setPointOfInterest)
                {
                    continue;
                }
                
                _model.PointOfInterest = _points.Get(setPointOfInterest.PointOfInterest);
                
                _view.NavMeshAgent.SetDestination(_model.PointOfInterest);
            }
        }
    }
}