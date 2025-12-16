using System;
using Runtime.Colony.Citizens;
using Runtime.Common;
using Runtime.Descriptions;
using Runtime.Descriptions.StateMachine.Actions;

namespace Runtime.Colony.StateMachine
{
    public class StateMachinePresenter : IPresenter
    {
        private readonly StateMachineModel _model;
        
        private readonly CitizenModel _citizenModel;
        
        private readonly WorldDescription _worldDescription;

        public StateMachinePresenter(StateMachineModel model, CitizenModel citizenModel, WorldDescription worldDescription)
        {
            _model = model;            
            
            _citizenModel =  citizenModel;
            
            _worldDescription =  worldDescription;
        }
        
        public void Enable()
        {
            _model.OnChange += OnChangeState;
        }

        public void Disable()
        {
            _model.OnChange -= OnChangeState;
        }

        private void OnChangeState()
        {
            foreach (var action in _model.CurrentState.Actions)
            {
                switch (action)
                {
                    case TimerActionDescription timerAction:
                        
                        _citizenModel.Timers[timerAction.Timer] = DateTimeOffset.UtcNow.AddSeconds(timerAction.Duration).ToUnixTimeSeconds();

                        break;
                    
                    case SetPointOfInterestActionDescription setPointOfInterestAction:

                        var point = _worldDescription.PointsOfInterest.Get(setPointOfInterestAction
                            .PointOfInterest);
                        
                        _citizenModel.SetPointOfInterest(point);
                        
                        break;
                }
            }
        }
    }
}