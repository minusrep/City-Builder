using System.Collections.Generic;
using System.Threading.Tasks;
using Runtime.Colony;
using Runtime.Common;
using Runtime.Selection;
using Runtime.Selection.SelectedPanel;
using Runtime.UI;
using Runtime.ViewDescriptions;
using UnityEngine.UIElements;

namespace Runtime.LoadSteps
{
    public class BuildingSelectionLoadStep : IStep
    {
        private readonly List<IPresenter> _presenters;
        private readonly World _world;
        private readonly WorldViewDescriptions _worldViewDescriptions;
        private readonly MenuContent _menuContent;
        private readonly SelectionView _selectionView;

        public BuildingSelectionLoadStep(List<IPresenter> presenters, World world,
            WorldViewDescriptions worldViewDescriptions, MenuContent menuContent, SelectionView selectionView)
        {
            _presenters = presenters;
            _world = world;
            _worldViewDescriptions = worldViewDescriptions;
            _menuContent = menuContent;
            _selectionView = selectionView;
        }

        public Task Run()
        {
            var buildingSelectionPresenter = new SelectionPresenter(_world.SelectionModel, _selectionView, _world);
            buildingSelectionPresenter.Enable();
            _presenters.Add(buildingSelectionPresenter);

            var selectionPanel = _worldViewDescriptions.SelectionViewDescription.SelectionPanelAsset.CloneTree().Q<VisualElement>("selection-panel");
            
            _menuContent.HudLayer.Add(selectionPanel);
            
            var buildingPanelView =
                new SelectedPanelView(selectionPanel,
                    _worldViewDescriptions.StatViewDescriptions, 
                    _worldViewDescriptions.InventoryViewDescription);
            var buildingPanelPresenter =
                new SelectedPanelPresenter(buildingPanelView, _world.SelectionModel, _world, _menuContent);
            
            
            buildingPanelPresenter.Enable();
            _presenters.Add(buildingPanelPresenter);

            return Task.CompletedTask;
        }
    }
}