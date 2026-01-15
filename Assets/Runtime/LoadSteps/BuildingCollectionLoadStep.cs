using System.Collections.Generic;
using System.Threading.Tasks;
using Runtime.Colony;
using Runtime.Colony.Buildings.Collection;
using Runtime.Common;
using Runtime.ViewDescriptions;

namespace Runtime.LoadSteps
{
    public class BuildingCollectionLoadStep : IStep
    {
        private readonly List<IPresenter> _presenters;
        private readonly World _world;
        private readonly BuildingCollectionView _buildingCollectionView;
        private readonly WorldViewDescriptions _worldViewDescriptions;

        public BuildingCollectionLoadStep(List<IPresenter> presenters, World world,
            BuildingCollectionView buildingCollectionView,
            WorldViewDescriptions worldViewDescriptions)
        {
            _presenters = presenters;
            _world = world;
            _buildingCollectionView = buildingCollectionView;
            _worldViewDescriptions = worldViewDescriptions;
        }
        
        public async Task Run()
        {
            var buildingCollectionPresenter = new BuildingCollectionPresenter(_world,
                _buildingCollectionView, _worldViewDescriptions);

            buildingCollectionPresenter.Enable();
            _presenters.Add(buildingCollectionPresenter);

            await Task.CompletedTask;
        }
    }
}