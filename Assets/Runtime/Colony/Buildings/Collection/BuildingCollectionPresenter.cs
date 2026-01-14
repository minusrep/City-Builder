using System.Collections.Generic;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Common.Factories;
using Runtime.Common;
using Runtime.Common.ObjectPool;
using Runtime.ViewDescriptions;

namespace Runtime.Colony.Buildings.Collection
{
    public class BuildingCollectionPresenter : IPresenter
    {
        private readonly World _world;
        private readonly BuildingCollectionView _view;
        private readonly WorldViewDescriptions _worldViewDescriptions;
        private readonly BuildingPresenterFactory _presenterFactory;

        private readonly Dictionary<string, IPresenter> _presenters = new();
        private readonly Dictionary<string, IObjectPool<BuildingView>> _viewPools = new();

        public BuildingCollectionPresenter(World world,
            BuildingCollectionView view,
            WorldViewDescriptions worldViewDescriptions)
        {
            _world = world;
            _view = view;
            _worldViewDescriptions = worldViewDescriptions;

            _presenterFactory = new BuildingPresenterFactory(_world, _viewPools, worldViewDescriptions);
        }

        public async void Enable()
        {
            foreach (var viewDescriptionBase in _worldViewDescriptions.BuildingViewDescriptions.Descriptions)
            {
                var prefab = await viewDescriptionBase.Prefab.LoadAssetAsync().Task;
                var buildingView = prefab.GetComponent<BuildingView>();
                _viewPools[viewDescriptionBase.Id] =
                    new ObjectPool<BuildingView>(buildingView, 2, _view.Transform);
                
                viewDescriptionBase.Prefab.ReleaseAsset();
            }
            
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