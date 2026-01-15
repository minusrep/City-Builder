using System.Collections.Generic;
using System.Threading.Tasks;
using Runtime.Colony;
using Runtime.Colony.Buildings.Construction;
using Runtime.Colony.Buildings.Construction.Menu;
using Runtime.Colony.Buildings.Construction.WorldGrid;
using Runtime.Common;
using Runtime.Descriptions;
using Runtime.UI;
using Runtime.ViewDescriptions;

namespace Runtime.LoadSteps
{
    public class BuildingConstructionLoadStep : IStep
    {
        private readonly List<IPresenter> _presenters;
        private readonly BuildingConstructionView _buildingConstructionView;
        private readonly WorldGridView _worldGridView;
        private readonly WorldDescription _worldDescription;
        private readonly World _world;
        private readonly WorldViewDescriptions _worldViewDescriptions;
        private readonly MenuContent _menuContent;

        public BuildingConstructionLoadStep(List<IPresenter> presenters,
            BuildingConstructionView buildingConstructionView, WorldGridView worldGridView,
            WorldDescription worldDescription, World world, WorldViewDescriptions worldViewDescriptions,
            MenuContent menuContent)
        {
            _buildingConstructionView = buildingConstructionView;
            _worldGridView = worldGridView;
            _worldDescription = worldDescription;
            _world = world;
            _worldViewDescriptions = worldViewDescriptions;
            _menuContent = menuContent;
            _presenters = presenters;
        }

        public async Task Run()
        {
            var worldGridPresenter = new WorldGridPresenter(_world.Grid, _worldGridView);
            worldGridPresenter.Enable();
            _presenters.Add(worldGridPresenter);
            
            var buildingConstructionPresenter = new BuildingConstructionPresenter(_world.BuildingConstructionModel,
                _buildingConstructionView, _world, _worldViewDescriptions);
            buildingConstructionPresenter.Enable();
            _presenters.Add(buildingConstructionPresenter);
            
            var buildingConstructionMenuView =
                new BuildingConstructionMenuView(_worldViewDescriptions.SelectionViewDescription.ConstructionMenuAsset);
            var buildingConstructionMenuPresenter = new BuildingConstructionMenuPresenter(buildingConstructionMenuView, _world,
                _worldDescription, _menuContent);
            buildingConstructionMenuPresenter.Enable();
            _presenters.Add(buildingConstructionMenuPresenter);
            
            await Task.CompletedTask;
        }
    }
}