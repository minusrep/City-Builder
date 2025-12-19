using System.Collections.Generic;
using System.Threading.Tasks;
using Runtime.Colony;
using Runtime.Colony.Buildings.Collection;
using Runtime.Common;
using Runtime.Descriptions;
using Runtime.GameSystems;
using Runtime.ViewDescriptions;

namespace Runtime.Services.SaveLoadSteps
{
    public class BuildingCollectionLoadStep : IStep
    {
        private readonly List<IPresenter> _presenters;
        
        private readonly World _world;
        private readonly BuildingCollectionView _buildingCollectionView;
        private readonly WorldDescription _worldDescription;
        private readonly WorldViewDescriptions _worldViewDescriptions;
        private readonly GameSystemCollection _gameSystemCollection;

        public BuildingCollectionLoadStep(List<IPresenter> presenters, World world, 
            BuildingCollectionView buildingCollectionView, WorldDescription worldDescription, 
            WorldViewDescriptions worldViewDescriptions, GameSystemCollection gameSystemCollection)
        {
            _presenters = presenters;
            _world = world;
            _buildingCollectionView = buildingCollectionView;
            _worldDescription = worldDescription;
            _worldViewDescriptions = worldViewDescriptions;
            _gameSystemCollection = gameSystemCollection;
        }


        public async Task Run()
        {
            var buildingCollectionPresenter = new BuildingCollectionPresenter(_world,
                _buildingCollectionView, _worldDescription.BuildingCollection, _worldViewDescriptions, _gameSystemCollection);
            
            buildingCollectionPresenter.Enable();
            _presenters.Add(buildingCollectionPresenter);
            
            await Task.CompletedTask;
        }
    }
}