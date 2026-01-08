using System.Collections.Generic;
using Runtime.Colony.Buildings.Production;
using Runtime.Colony.Buildings.Service;
using Runtime.Colony.Buildings.Storage;
using Runtime.Common.ObjectPool;
using Runtime.GameSystems;
using Runtime.ViewDescriptions;

namespace Runtime.Colony.Buildings.Common.Factories
{
    public class BuildingPresenterFactory
    {
        private readonly WorldViewDescriptions _worldViewDescriptions;
        private readonly Dictionary<string, IObjectPool<BuildingView>>  _viewPools;

        public BuildingPresenterFactory(WorldViewDescriptions worldViewDescriptions, Dictionary<string, IObjectPool<BuildingView>> viewPools)
        {
            _worldViewDescriptions = worldViewDescriptions;
            _viewPools = viewPools;
        }
        
        public BuildingPresenter Create(BuildingModel model)
        {
            var viewId = model.BaseDescription.ViewDescriptionId;
            var pool = _viewPools[viewId];

            return model switch
            {
                ProductionBuildingModel productionModel => new ProductionBuildingPresenter(productionModel, pool,
                    _worldViewDescriptions),
                StorageBuildingModel storageModel => new StorageBuildingPresenter(storageModel, pool, _worldViewDescriptions),
                ServiceBuildingModel serviceModel => new ServiceBuildingPresenter(serviceModel, pool, _worldViewDescriptions),
                _ => new BuildingPresenter(model, pool, _worldViewDescriptions),
            };
        }
    }
}