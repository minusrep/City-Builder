using System.Collections.Generic;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Common.Factories;
using Runtime.Colony.Buildings.Pool;
using Runtime.Common;
using Runtime.Descriptions.Buildings;

namespace Runtime.Colony.Buildings.Collection
{
    public class BuildingCollectionPresenter : IPresenter
    {
        private readonly BuildingModelCollection _models;
        private readonly BuildingPresenterFactory _presenterFactory;
        
        private readonly Dictionary<string, IPresenter> _presenters = new();

        public BuildingCollectionPresenter(BuildingModelCollection models,
            BuildingCollectionView view,
            BuildingsDescriptionCollection modelDescriptions, 
            ViewDescriptions.ViewDescriptions viewDescriptions)
        {
            _models = models;
            
            var poolRegistry = new BuildingPoolRegistry();
            poolRegistry.RegisterAll(viewDescriptions.BuildingViewDescriptions, modelDescriptions, view.Transform);
            
            _presenterFactory = new BuildingPresenterFactory(viewDescriptions, poolRegistry);
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