using System.Collections.Generic;
using System.Threading.Tasks;
using Runtime.Colony;
using Runtime.Colony.Buildings.Selection;
using Runtime.Common;
using Runtime.UI.HUD.BuildingPanel;
using Runtime.ViewDescriptions;

namespace Runtime.LoadSteps
{
    public class BuildingSelectionLoadStep : IStep
    {
        private readonly List<IPresenter> _presenters;
        private readonly World _world;
        private readonly WorldViewDescriptions _worldViewDescriptions;

        public BuildingSelectionLoadStep(List<IPresenter> presenters, World world, WorldViewDescriptions worldViewDescriptions)
        {
            _presenters = presenters;
            _world = world;
            _worldViewDescriptions = worldViewDescriptions;
        }

        public Task Run()
        {
            var buildingSelectionPresenter = new BuildingSelectionPresenter(_world.BuildingSelectionModel, _world);
            buildingSelectionPresenter.Enable();
            _presenters.Add(buildingSelectionPresenter);

            var buildingPanelView = new BuildingPanelView(_worldViewDescriptions.BuildingMenuViewDescription.BuildingPanelAsset);
            var buildingPanelPresenter = new BuildingPanelPresenter(_world.BuildingSelectionModel, buildingPanelView, _world);
            buildingPanelPresenter.Enable();
            _presenters.Add(buildingPanelPresenter);
            
            return Task.CompletedTask;
        }
    }
}