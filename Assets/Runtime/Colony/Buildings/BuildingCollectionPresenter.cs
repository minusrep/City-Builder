using System.Collections.Generic;
using Runtime.Colony.Buildings.Common;
using Runtime.ViewDescriptions.Buildings;
using IPresenter = Runtime.Core.IPresenter;

namespace Runtime.Colony.Buildings
{
    public class BuildingCollectionPresenter : IPresenter
    {
        private readonly BuildingModelCollection _models;
        private readonly BuildingCollectionView _view;
        private readonly BuildingViewDescriptionCollection _viewDescriptions;
        private readonly Dictionary<string, BuildingPresenter> _presenters = new();

        public BuildingCollectionPresenter(BuildingModelCollection models, BuildingViewDescriptionCollection viewDescriptions, BuildingCollectionView view)
        {
            _models = models;
            _view = view;
            _viewDescriptions = viewDescriptions;
        }

        public void Enable()
        {
            _models.OnAdded += HandleAdded;
            _models.OnRemoved += HandleRemoved;

            foreach (var model in _models.Models.Values)
            {
                HandleAdded(model);
            }
        }

        public void Disable()
        {
            _models.OnAdded -= HandleAdded;
            _models.OnRemoved -= HandleRemoved;

            foreach (var presenter in _presenters.Values)
            {
                presenter.Disable();
            }
        }

        private void HandleAdded(BuildingModel model)
        {
            var presenter = new BuildingPresenter(model, _viewDescriptions.Get(model.BaseDescription.ViewDescriptionId), _view.Transform);
            presenter.Enable();
            _presenters.Add(model.Id, presenter);
        }
        
        private void HandleRemoved(BuildingModel model)
        {
            var presenter = _presenters[model.Id];
            presenter.Disable();
            _presenters.Remove(model.Id);
        }
    }
}