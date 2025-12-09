using System;
using Runtime.Core;
using Runtime.StateMachine;
using Runtime.StateMachine.Descriptions;

namespace Runtime.Colony.Citizens.Timer
{
    public class TimerPresenter : IPresenter
    {
        private readonly ITimerModel _model;
        
        private readonly ITimerView _view;

        private readonly StateMachineModel _stateMachineModel;
        
        public TimerPresenter(ITimerModel model, ITimerView view, StateMachineModel stateMachineModel)
        {
            _model = model;
            
            _view = view;
            
            _stateMachineModel = stateMachineModel;
        }


        public void Enable()
        {
            _stateMachineModel.OnChange += OnChangeState;
        }

        public void Disable()
        {
            _stateMachineModel.OnChange -= OnChangeState;
        }

        private void OnChangeState()
        {
            foreach (var action in _stateMachineModel.CurrentState.Actions)
            {
                if (action is not TimerActionDescription timerAction)
                {
                    continue;
                }
                
                _model.Timers[timerAction.Timer] = DateTimeOffset.UtcNow.AddSeconds(timerAction.Duration).ToUnixTimeSeconds();
            }
        }
    }
}