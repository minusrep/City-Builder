using System;
using Runtime.Colony.Citizens;
using Runtime.Common;
using Runtime.Descriptions.StateMachine.Actions;

namespace Runtime.Colony.StateMachine
{
    public class CitizenTimerPresenter : IPresenter
    {
        private readonly CitizenModel _model;

        public CitizenTimerPresenter(CitizenModel model)
        {
            _model = model;
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
                if (action is not TimerActionDescription timerAction)
                {
                    continue;
                }
                
                _model.Timers[timerAction.Timer] = DateTimeOffset.UtcNow.AddSeconds(timerAction.Duration).ToUnixTimeSeconds();

                break;
            }
        }
    }
}