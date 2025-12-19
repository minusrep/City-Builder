using System.Collections.Generic;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Common.Factories;
using Runtime.Colony.Buildings.Pool;
using Runtime.Common;
using Runtime.Descriptions.Buildings;
using Runtime.GameSystems;
using Runtime.ViewDescriptions;

namespace Runtime.Colony.Buildings.Collection
{
    public class BuildingCollectionPresenter : IPresenter
    {
        private readonly World _world;
        private readonly BuildingPresenterFactory _presenterFactory;
        
        private readonly Dictionary<string, IPresenter> _presenters = new();

        public BuildingCollectionPresenter(World world,
            BuildingCollectionView view,
            BuildingsDescriptionCollection modelDescriptions, 
            WorldViewDescriptions worldViewDescriptions,
            GameSystemCollection gameSystemCollection)
        {
            _world = world;
            var poolRegistry = new BuildingPoolRegistry();
            poolRegistry.RegisterAll(worldViewDescriptions.BuildingViewDescriptions, modelDescriptions, view.Transform);
            
            _presenterFactory = new BuildingPresenterFactory(world, gameSystemCollection, poolRegistry, worldViewDescriptions);
        }

        public void Enable()
        {
            _world.Buildings.OnAdded += HandleAdded;
            _world.Buildings.OnRemoved += HandleRemoved;

            foreach (var model in _world.Buildings.Models.Values)
            {
                HandleAdded(model);
            }
        }

        public void Disable()
        {
            _world.Buildings.OnAdded -= HandleAdded;
            _world.Buildings.OnRemoved -= HandleRemoved;

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