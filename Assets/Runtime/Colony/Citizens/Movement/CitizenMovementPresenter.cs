using Runtime.Colony.StateMachine;
using Runtime.Common;
using Runtime.Descriptions;
using Runtime.Descriptions.StateMachine.Actions;
using Runtime.Services.Update;

namespace Runtime.Colony.Citizens.Movement
{
    public class CitizenMovementPresenter : IPresenter
    {
        private readonly CitizenModel _model;
        
        private readonly MovementView _view;
        
        private readonly PointOfInterestDescriptionCollection _points;
        
        private readonly UpdateService _updateService;

        public CitizenMovementPresenter(CitizenModel model, MovementView view, UpdateService updateService)
        {
            _updateService = updateService;

            _view = view;

            _model = model;
        }

        public void Enable()
        {
            _updateService.OnUpdate += Update;

            _model.StateMachine.OnChange += OnChangeState;
            
            
            _view.NavMeshAgent.isStopped = false;
        }

        public void Disable()
        {
            _updateService.OnUpdate -= Update;
            
            _model.StateMachine.OnChange -= OnChangeState;
            
            _view.NavMeshAgent.isStopped = true;
        }

        private void Update()
        {
            _model.Position = _view.Transform.position;
        }

        private void OnChangeState()
        {
            var currentState = _model.StateMachine.CurrentState;

            foreach (var action in currentState.Actions)
            {
                if (action is not StartMoveActionDescription startMoveAction)
                {
                    continue;
                }
                
                _view.NavMeshAgent.SetDestination(_model.PointsOfInterest[startMoveAction.PointOfInterest]);

                break;
            }
        }
    }
}