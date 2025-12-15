using Runtime.Colony.Buildings.Pool;
using Runtime.Colony.Buildings.Production;
using Runtime.Colony.Buildings.Service;
using Runtime.Colony.Buildings.Storage;
using Runtime.Common;

namespace Runtime.Colony.Buildings.Common.Factories
{
    public class BuildingPresenterFactory
    { 
        private readonly ViewDescriptions.ViewDescriptions _viewDescriptions;
        private readonly BuildingPoolRegistry _pools;

        public BuildingPresenterFactory(ViewDescriptions.ViewDescriptions viewDescriptions, BuildingPoolRegistry pools)
        {
            _viewDescriptions = viewDescriptions;
            _pools = pools;
        }
        
        public IPresenter Create(BuildingModel model)
        {
            var viewId = model.BaseDescription.ViewDescriptionId;
            var pool = _pools.Get(viewId);

            return model switch
            {
                ProductionBuildingModel productionModel => new ProductionBuildingPresenter(productionModel, pool,
                    _viewDescriptions),
                StorageBuildingModel storageModel =>
                    new StorageBuildingPresenter(storageModel, pool, _viewDescriptions),
                ServiceBuildingModel serviceModel =>
                    new ServiceBuildingPresenter(serviceModel, pool, _viewDescriptions),
                _ => new BuildingPresenter<BuildingView>(model, pool, _viewDescriptions)
            };
        }
    }
}