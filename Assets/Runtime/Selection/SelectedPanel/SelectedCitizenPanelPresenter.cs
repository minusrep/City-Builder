using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Stats;
using Runtime.Common;

namespace Runtime.Selection.SelectedPanel
{
    public class SelectedCitizenPanelPresenter : IPresenter
    {
        private const string CitizenPrefixId = "citizen_";
        
        private readonly SelectedPanelView _view;
        private readonly SelectionModel _model;
        private readonly World _world;

        private List<StatPresenter> _statPresenters = new List<StatPresenter>();

        public SelectedCitizenPanelPresenter(SelectedPanelView view, SelectionModel model, World world)
        {
            _view = view;
            _model = model;
            _world = world;
        }
        
        public void Enable()
        {
            _model.OnChange += OnChange;
        }

        public void Disable()
        {
            _model.OnChange -= OnChange;
        }

        private void OnChange()
        {
            TryDrawCitizenModel();
        }

        private void TryDrawCitizenModel()
        {
            if (!_world.Citizens.TryGet($"{CitizenPrefixId}{_model.SelectedId}", out var selectedCitizen))
            {
                return;
            }
            
            SelectionPanelUtility.SetupPanel(_view.Root);

            foreach (var presenters in _statPresenters)
            {
                presenters.Disable();
            }

            _statPresenters = new List<StatPresenter>();
            
            _view.Root.Clear();
            
            _view.Root.Add(SelectionPanelUtility.CreateTitle(selectedCitizen.Id.ToString()));

            foreach (var stat in selectedCitizen.Stats)
            {
                var statView = new StatView(_view.StatViewDescriptions[stat.Stat.ViewId]);
                
                var statPresenter = new StatPresenter(stat, statView);
                
                _statPresenters.Add(statPresenter);

                statPresenter.Enable();

                _view.Root.Add(statView.Root);
            }
        }

    }
}