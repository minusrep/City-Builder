using System.Collections.Generic;
using Runtime.Colony.Buildings.Common;
using Runtime.Common;
using Runtime.Common.ObjectPool;
using Runtime.ViewDescriptions.Buildings;

namespace Runtime.Colony.Buildings.Collection
{
    public class BuildingCollectionPresenter : IPresenter
    {
        private readonly BuildingModelCollection _models;
        private readonly BuildingViewDescriptionCollection _viewDescriptions;
        
        private readonly Dictionary<string, BuildingPresenter> _presenters = new();
        private readonly Dictionary<string, ObjectPool<BuildingView>> _viewPools = new();

        public BuildingCollectionPresenter(BuildingModelCollection models, BuildingViewDescriptionCollection viewDescriptions, BuildingCollectionView view)
        {
            _models = models;
            _viewDescriptions = viewDescriptions;
            
            foreach (var description in _viewDescriptions.Descriptions)
            {
                _viewPools[description.name] = new ObjectPool<BuildingView>(description.Prefab, 2, view.Transform);
            }
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
            var presenter = new BuildingPresenter(model, _viewPools[model.BaseDescription.ViewDescriptionId],
                _viewDescriptions.Get(model.BaseDescription.ViewDescriptionId));
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