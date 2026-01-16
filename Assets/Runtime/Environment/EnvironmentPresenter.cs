using Runtime.Common;

namespace Runtime.Environment
{
    public class EnvironmentPresenter : IPresenter
    {
        private readonly EnvironmentModel _model;
        private readonly EnvironmentView _view;
        
        private EnvironmentTimePresenter _timePresenter;

        public EnvironmentPresenter(EnvironmentModel model, EnvironmentView view)
        {
            _model = model;
            _view = view;
        }
        
        public void Enable()
        {
            _timePresenter = new EnvironmentTimePresenter(_model.Time, _view);
            
            _timePresenter.Enable();
        }

        public void Disable()
        {
            _timePresenter?.Disable();
            
            _timePresenter = null;
        }
    }
}