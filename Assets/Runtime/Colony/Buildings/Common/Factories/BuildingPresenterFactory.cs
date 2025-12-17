using Runtime.Colony.Buildings.Pool;
using Runtime.Colony.Buildings.Production;
using Runtime.Colony.Buildings.Service;
using Runtime.Colony.Buildings.Storage;
using Runtime.Common;
using Runtime.GameSystems;

namespace Runtime.Colony.Buildings.Common.Factories
{
    public class BuildingPresenterFactory
    {
        private readonly World _world;
        private readonly ViewDescriptions.ViewDescriptions _viewDescriptions;
        private readonly BuildingPoolRegistry _pools;
        private readonly GameSystemCollection _gameSystemCollection;

        public BuildingPresenterFactory(World world, GameSystemCollection gameSystemCollection,
            BuildingPoolRegistry pools, ViewDescriptions.ViewDescriptions viewDescriptions)
        {
            _world = world;
            _viewDescriptions = viewDescriptions;
            _pools = pools;
            _gameSystemCollection = gameSystemCollection;
        }
        
        public IPresenter Create(BuildingModel model)
        {
            var viewId = model.BaseDescription.ViewDescriptionId;
            var pool = _pools.Get(viewId);

            return model switch
            {
                ProductionBuildingModel productionModel => new ProductionBuildingPresenter(productionModel, pool,
                    _viewDescriptions, _gameSystemCollection),
                StorageBuildingModel storageModel =>
                    new StorageBuildingPresenter(storageModel, pool, _viewDescriptions),
                ServiceBuildingModel serviceModel =>
                    new ServiceBuildingPresenter(serviceModel, pool, _world, _viewDescriptions, _gameSystemCollection),
                _ => new BuildingPresenter<BuildingView>(model, pool, _viewDescriptions)
            };
        }
    }
}