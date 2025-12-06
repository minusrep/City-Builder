namespace Runtime.StateMachine
{
    public class StateMachinePresenter
    {
        private StateMachineModel _model;

        private StateMachineView _view;

        public StateMachinePresenter(StateMachineModel model, StateMachineView view)
        {
            _model = model;
            
            _view = view;
        }
        
        public void Enable()
        {
            
        }

        public void Disable()
        {
            
        }
    }
}