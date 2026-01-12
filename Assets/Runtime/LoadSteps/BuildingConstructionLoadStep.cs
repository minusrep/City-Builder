using System.Threading.Tasks;
using Runtime.Colony;
using Runtime.Colony.Buildings.Construction;
using Runtime.Colony.Buildings.Construction.Menu;
using Runtime.Colony.Buildings.Construction.WorldGrid;
using Runtime.Descriptions;
using Runtime.UI;
using Runtime.ViewDescriptions;
using UnityEngine.UIElements;

namespace Runtime.LoadSteps
{
    public class BuildingConstructionLoadStep : IStep
    {
        private readonly VisualTreeAsset _constructionMenuAsset;
        private readonly BuildingConstructionView _buildingConstructionView;
        private readonly WorldGridView _worldGridView;
        private readonly WorldDescription _worldDescription;
        private readonly World _world;
        private readonly WorldViewDescriptions _worldViewDescriptions;
        private readonly MenuContent _menuContent;

        public BuildingConstructionLoadStep(VisualTreeAsset constructionMenuAsset,
            BuildingConstructionView buildingConstructionView, WorldGridView worldGridView,
            WorldDescription worldDescription, World world, WorldViewDescriptions worldViewDescriptions,
            MenuContent menuContent)
        {
            _constructionMenuAsset = constructionMenuAsset;
            _buildingConstructionView = buildingConstructionView;
            _worldGridView = worldGridView;
            _worldDescription = worldDescription;
            _world = world;
            _worldViewDescriptions = worldViewDescriptions;
            _menuContent = menuContent;
        }

        public async Task Run()
        {
            var worldGridPresenter = new WorldGridPresenter(_world.Grid, _worldGridView);
            worldGridPresenter.Enable();
            
            var buildingConstructionPresenter = new BuildingConstructionPresenter(_world.BuildingConstructionModel,
                _buildingConstructionView, _world, _worldViewDescriptions);
            buildingConstructionPresenter.Enable();

            var buildingConstructionMenuView =
                new BuildingConstructionMenuView(_constructionMenuAsset);
            var buildingConstructionMenuPresenter = new BuildingConstructionMenuPresenter(buildingConstructionMenuView, _world,
                _worldDescription, _menuContent);
            buildingConstructionMenuPresenter.Enable();
            await Task.CompletedTask;
        }
    }
}