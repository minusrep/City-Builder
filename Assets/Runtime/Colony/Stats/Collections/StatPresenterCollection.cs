using System.Collections.Generic;
using Runtime.Common;
using Runtime.ViewDescriptions.Stats;

namespace Runtime.Colony.Stats.Collections
{
    public class StatPresenterCollection : IPresenter
    {
        private readonly StatModelCollection _model;
        
        private readonly StatViewCollection _view;
        
        private readonly StatViewDescriptionCollection _statViewDescriptions;

        private readonly List<StatPresenter> _presenters = new List<StatPresenter>() ;
        
        public StatPresenterCollection(StatViewCollection view, StatModelCollection model,
            StatViewDescriptionCollection statViewDescriptions)
        {
            _model = model;
            _view = view;
            _statViewDescriptions = statViewDescriptions;
        }

        public void Enable()
        {
            foreach (var stat in _model)
            {
                var statView = new StatView(_statViewDescriptions[stat.Stat.ViewId]);
                _view.Root.Add(statView.Root);
                
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