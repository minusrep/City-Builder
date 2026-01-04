using System.Collections.Generic;
using Runtime.Colony.Buildings.Production;
using Runtime.Colony.Buildings.Storage;
using Runtime.Common.ObjectPool;
using Runtime.GameSystems;
using Runtime.ViewDescriptions;

namespace Runtime.Colony.Buildings.Common.Factories
{
    public class BuildingPresenterFactory
    {
        private readonly WorldViewDescriptions _worldViewDescriptions;
        private readonly GameSystemCollection _gameSystemCollection;
        private readonly Dictionary<string, IObjectPool<BuildingView>>  _viewPools;

        public BuildingPresenterFactory(GameSystemCollection gameSystemCollection, WorldViewDescriptions worldViewDescriptions, Dictionary<string, IObjectPool<BuildingView>> viewPools)
        {
            _worldViewDescriptions = worldViewDescriptions;
            _viewPools = viewPools;
            _gameSystemCollection = gameSystemCollection;
        }
        
        public BuildingPresenter Create(BuildingModel model)
        {
            var viewId = model.BaseDescription.ViewDescriptionId;
            var pool = _viewPools[viewId];

            return model switch
            {
                ProductionBuildingModel productionModel => new ProductionBuildingPresenter(productionModel, pool,
                    _worldViewDescriptions, _gameSystemCollection),
                StorageBuildingModel storageModel => new StorageBuildingPresenter(storageModel, pool, _worldViewDescriptions),
                _ => new BuildingPresenter(model, pool, _worldViewDescriptions)
            };
        }
    }
}