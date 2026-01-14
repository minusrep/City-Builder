using Runtime.Colony;
using Runtime.Common;
using Runtime.Input;
using Runtime.UI.HUD.BuildingPanel;
using Runtime.UI.HUD.BuildingSelection;

namespace Runtime.UI.HUD
{
    public class HUDPresenter : IPresenter
    {
        private readonly HUDView _view;
        
        private readonly HUDModel _model;

        private readonly World _world;

        private BuildingSelectionPresenter _buildingSelectionPresenter;

        private BuildingPanelPresenter _buildingPanelPresenter;
        
        public HUDPresenter(HUDView view, HUDModel model, World world)
        {
            _view = view;
            _model = model;
            _world = world;
        }

        public void Enable()
        {
            _buildingSelectionPresenter = new BuildingSelectionPresenter(_view.BuildingSelectionView, _model.BuildingSelectionModel, _world);
            
            _buildingPanelPresenter = new BuildingPanelPresenter(_view, _model.BuildingSelectionModel, _world);
            
            _buildingSelectionPresenter.Enable();
            
            _buildingPanelPresenter.Enable();
        }

        public void Disable()
        {
            _buildingSelectionPresenter.Disable();

            _buildingPanelPresenter.Disable();
            
            _buildingSelectionPresenter = null;
            
            _buildingPanelPresenter = null;
        }
    }
}