using Runtime.Colony.Buildings.Production;
using System.Collections.Generic;
using Runtime.Colony.Buildings.Storage;
using Runtime.Common.ObjectPool;

namespace Runtime.Colony.Buildings.Common.Factories
{
    public class BuildingPresenterFactory
    {
        private readonly ViewDescriptions.ViewDescriptions _viewDescriptions;
        private readonly Dictionary<string, ObjectPool<BuildingView>> _viewPools;

        public BuildingPresenterFactory(ViewDescriptions.ViewDescriptions viewDescriptions, Dictionary<string, ObjectPool<BuildingView>> viewPools)
        {
            _viewDescriptions = viewDescriptions;
            _viewPools = viewPools;
        }
        
        public BuildingPresenter Create(BuildingModel model)
        {
            var viewId = model.BaseDescription.ViewDescriptionId;
            var pool = _viewPools[viewId];

            if (model is ProductionBuildingModel productionModel)
            {
                return new ProductionBuildingPresenter(productionModel, pool, _viewDescriptions);
            }

            if (model is StorageBuildingModel storageModel)
            {
                return new StorageBuildingPresenter(storageModel, pool, _viewDescriptions);
            }

            return new BuildingPresenter(model, pool, _viewDescriptions);
        }
    }
}