using fastJSON;
using Runtime.Common;
using Runtime.Descriptions;
using UnityEngine;

namespace Runtime.Colony.Citizens.Movement
{
    public class CitizenMovementPresenter : IPresenter
    {
        private readonly CitizenModel _model;
        
        private readonly CitizenMovementView _view;
        
        private readonly PointOfInterestDescriptionCollection _points;
        
        public CitizenMovementPresenter(CitizenModel model, CitizenMovementView view)
        {
            _view = view;

            _model = model;
        }

        public void Enable()
        {
            _view.OnUpdate += UpdatePosition;
            
            _model.OnStartMove += StartMove;
            
            _view.NavMeshAgent.isStopped = false;
        }

        public void Disable()
        {
            _view.OnUpdate -= UpdatePosition;
            
            _model.OnStartMove -= StartMove;

            _view.NavMeshAgent.isStopped = true;
        }

        private void UpdatePosition()
        {
            _model.Position = _view.Transform.position;
        }

        private void StartMove(string pointOfInterest)
        {
            _view.NavMeshAgent.SetDestination(_model.PointsOfInterest[pointOfInterest]);
        }
    }
}