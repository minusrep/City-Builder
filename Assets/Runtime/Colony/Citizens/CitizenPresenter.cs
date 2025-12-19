using Runtime.Colony.Citizens.Movement;
using Runtime.Common;
using UnityEngine;

namespace Runtime.Colony.Citizens
{
    public class CitizenPresenter : IPresenter
    {
        private CitizenMovementPresenter _citizenMovementPresenter;

        private CitizenDebugPresenter _citizenDebugPresenter;
        
        private readonly CitizenModel _model;
        
        private readonly World _world;
        
        private readonly CitizenView _view;
        
        public CitizenPresenter(CitizenView view,  CitizenModel model, World world)
        {
            _view =  view;
            
            _world =  world;
            
            _model = model;

            _model.StateMachine.OnChange += OnChangeState;
        }

        public void Enable()
        {
            _citizenMovementPresenter = new CitizenMovementPresenter(_model, _view.CitizenMovementView);

            _citizenDebugPresenter = new CitizenDebugPresenter(_view, _model);
            
            _citizenMovementPresenter.Enable();
            
            _citizenDebugPresenter.Enable();
            
            ExecuteActions();
        }

        public void Disable()
        {
            _citizenMovementPresenter.Disable();

            _citizenMovementPresenter = null;
        }

        private void OnChangeState()
        {
            ExecuteActions();
        }

        private void ExecuteActions()
        {
            foreach (var action in _model.StateMachine.CurrentState.Actions)
            {
                action.Execute(_world, _model);
            }
        }
    }
}