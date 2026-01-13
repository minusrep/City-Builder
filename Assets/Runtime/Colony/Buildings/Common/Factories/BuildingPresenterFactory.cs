using System.Collections.Generic;
using Runtime.Colony.Buildings.Production;
using Runtime.Colony.Buildings.Storage;
using Runtime.Common.ObjectPool;
using Runtime.ViewDescriptions;

namespace Runtime.Colony.Buildings.Common.Factories
{
    public class BuildingPresenterFactory
    {
        private readonly WorldViewDescriptions _worldViewDescriptions;
        private readonly World _world;
        private readonly Dictionary<string, IObjectPool<BuildingView>> _viewPools;

        public BuildingPresenterFactory(World world, Dictionary<string, IObjectPool<BuildingView>> viewPools,
            WorldViewDescriptions worldViewDescriptions)
        {
            _worldViewDescriptions = worldViewDescriptions;
            _viewPools = viewPools;
            _world = world;
        }

        public BuildingPresenter Create(BuildingModel model)
        {
            var viewId = model.BaseDescription.ViewDescriptionId;
            var pool = _viewPools[viewId];

            return model switch
            {
                ProductionBuildingModel productionModel => new ProductionBuildingPresenter(productionModel, pool, _world, _worldViewDescriptions),
                StorageBuildingModel storageModel => new StorageBuildingPresenter(storageModel, pool,
                    _worldViewDescriptions),
                _ => new BuildingPresenter(model, pool, _worldViewDescriptions)
            };
        }
    }
}