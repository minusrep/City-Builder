using Runtime.Colony.Citizens;
using Runtime.Common;
using Runtime.Descriptions.StateMachine.Actions;

namespace Runtime.Colony.StateMachine
{
    public class CitizenSetPointOfInterestPresenter : IPresenter
    {
        private readonly CitizenModel _model;
        
        private readonly World _world;

        public CitizenSetPointOfInterestPresenter(CitizenModel model, World world)
        {
            _model = model;

            _world = world;
        }
        
        public void Enable()
        {
            _model.StateMachine.OnChange += OnChangeState;
        }

        public void Disable()
        {
            _model.StateMachine.OnChange -= OnChangeState;
        }

        private void OnChangeState()
        {
            foreach (var action in _model.StateMachine.CurrentState.Actions)
            {
                if (action is not SetPointOfInterestActionDescription setPointOfInterest)
                {
                    continue;
                }

                var point = _world.WorldDescription.PointOfInterestCollection.Get(setPointOfInterest.PointOfInterest);
 
                _model.SetPointOfInterest(setPointOfInterest.PointOfInterest, point);
                        
                break;
            }
        }
    }
}