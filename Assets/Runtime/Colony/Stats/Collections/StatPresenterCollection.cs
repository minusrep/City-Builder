using System.Collections.Generic;
using Runtime.Common;
using Runtime.ViewDescriptions.Stats;

namespace Runtime.Colony.Stats.Collections
{
    public class StatPresenterCollection : IPresenter
    {
        private readonly StatModelCollection _stats;
        
        private readonly StatViewCollection _statsView;
        
        private readonly StatViewDescriptionCollection _statViewDescriptions;

        private readonly List<StatPresenter> _presenters = new List<StatPresenter>() ;
        
        public StatPresenterCollection(StatModelCollection stats, StatViewCollection statsView,
            StatViewDescriptionCollection statViewDescriptions)
        {
            _stats = stats;
            _statsView = statsView;
            _statViewDescriptions = statViewDescriptions;
        }

        public void Enable()
        {
            foreach (var stat in _stats)
            {
                var statView = new StatView(_statViewDescriptions[stat.Stat.ViewId]);
                _statsView.Root.Add(statView.Root);
                
                var statPresenter = new StatPresenter(stat, statView);
                statPresenter.Enable();
                _presenters.Add(statPresenter);
            }
        }

        public void Disable()
        {
            _presenters.Reverse();
            foreach (var presenter in _presenters)
            {
                presenter.Disable();
            }
            _presenters.Clear();
        }
    }
}