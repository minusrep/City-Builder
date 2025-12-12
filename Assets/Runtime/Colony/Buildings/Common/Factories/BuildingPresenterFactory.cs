using Runtime.Colony.Buildings.Production;
using Runtime.ViewDescriptions.Buildings;
using System.Collections.Generic;
using Runtime.Common.ObjectPool;

namespace Runtime.Colony.Buildings.Common.Factories
{
    public class BuildingPresenterFactory
    {
        private readonly BuildingViewDescriptionCollection _viewDescriptions;
        private readonly Dictionary<string, ObjectPool<BuildingView>> _viewPools;

        public BuildingPresenterFactory(BuildingViewDescriptionCollection viewDescriptions, Dictionary<string, ObjectPool<BuildingView>> viewPools)
        {
            _viewDescriptions = viewDescriptions;
            _viewPools = viewPools;
        }
        
        public BuildingPresenter Create(BuildingModel model)
        {
            var viewId = model.BaseDescription.ViewDescriptionId;
            var pool = _viewPools[viewId];
            var viewDescription = _viewDescriptions.Get(viewId);

            if (model is ProductionBuildingModel productionModel)
            {
                return new ProductionBuildingPresenter(productionModel, pool, viewDescription);
            }

            return new BuildingPresenter(model, pool, viewDescription);
        }
    }
}