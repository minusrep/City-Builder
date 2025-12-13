using System.Collections.Generic;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Common.Factories;
using Runtime.Common;
using Runtime.Common.ObjectPool;

namespace Runtime.Colony.Buildings.Collection
{
    public class BuildingCollectionPresenter : IPresenter
    {
        private readonly BuildingModelCollection _models;
        private readonly ViewDescriptions.ViewDescriptions _viewDescriptions;
        private readonly BuildingPresenterFactory _presenterFactory;
        
        private readonly Dictionary<string, BuildingPresenter> _presenters = new();
        private readonly Dictionary<string, ObjectPool<BuildingView>> _viewPools = new();

        public BuildingCollectionPresenter(BuildingModelCollection models, ViewDescriptions.ViewDescriptions viewDescriptions, BuildingCollectionView view)
        {
            _models = models;
            _viewDescriptions = viewDescriptions;

            
            foreach (var description in _viewDescriptions.BuildingViewDescriptions.Descriptions)
            {
                _viewPools[description.name] = new ObjectPool<BuildingView>(description.Prefab, 2, view.Transform);
            }
            
            _presenterFactory = new BuildingPresenterFactory(_viewDescriptions, _viewPools);
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
            var presenter = _presenterFactory.Create(model);
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