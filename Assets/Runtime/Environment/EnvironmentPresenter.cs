using Runtime.Common;

namespace Runtime.Environment
{
    public class EnvironmentPresenter : IPresenter
    {
        private readonly EnvironmentModel _model;
        private readonly EnvironmentView _view;
        private readonly EnvironmentSystem _system;

        private EnvironmentTimePresenter _timePresenter;

        public EnvironmentPresenter(EnvironmentModel model, EnvironmentView view, EnvironmentSystem system)
        {
            _model = model;
            _view = view;
            _system = system;
        }
        
        public void Enable()
        {
            _system.Register(_model);

            _timePresenter = new EnvironmentTimePresenter(_model.Time, _view);
            
            _timePresenter.Enable();
        }

        public void Disable()
        {
            _system.Unregister(_model);
            
            _timePresenter?.Disable();
            
            _timePresenter = null;
        }
    }
}