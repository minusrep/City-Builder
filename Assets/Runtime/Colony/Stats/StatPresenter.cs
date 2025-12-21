using Runtime.Common;

namespace Runtime.Colony.Stats
{
    public class StatPresenter : IPresenter
    {
        private StatModel _stat;
        
        private StatView _statView;

        public StatPresenter(StatModel stat, StatView statView)
        {
            _stat = stat;
            _statView = statView;
        }

        public void Enable()
        {
            _statView.Text = _stat.Stat.Id;
            
            _stat.ValueChanged += OnStatValueChanged;
            OnStatValueChanged(_stat.Value);
        }

        public void Disable()
        {
            _stat.ValueChanged -= OnStatValueChanged;
        }

        private void OnStatValueChanged(float value)
        {
            _statView.Value = value;
        }
    }
}