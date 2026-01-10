using Runtime.Colony;
using Runtime.Common;
using Runtime.Input;

namespace Runtime.UI.HUD
{
    public class HUDPresenter : IPresenter
    {
        private readonly HUDView _view;
        
        private readonly HUDModel _model;

        private World _world;

        private readonly PlayerControls _playerControls;

        private BuildingSelectionPresenter _buildingSelectionPresenter;
        
        public HUDPresenter(HUDView view, HUDModel model, World world, PlayerControls playerControls)
        {
            _view = view;
            _model = model;
            _world = world;
            _playerControls = playerControls;
        }

        public void Enable()
        {
            _buildingSelectionPresenter = new BuildingSelectionPresenter(_view.BuildingSelectionView, _model.BuildingSelectionModel, _playerControls);
            
            _buildingSelectionPresenter.Enable();
        }

        public void Disable()
        {
            _buildingSelectionPresenter.Disable();

            _buildingSelectionPresenter = null;
        }
    }
}