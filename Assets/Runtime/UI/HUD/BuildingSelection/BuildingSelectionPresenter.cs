using Runtime.Colony;
using Runtime.Colony.Buildings.Common;
using Runtime.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.UI.HUD.BuildingSelection
{
    public class BuildingSelectionPresenter : IPresenter
    {
        private readonly BuildingSelectionView _view;
        private readonly BuildingSelectionModel _model;
        private readonly World _world;

        private BuildingView _cachedBuildingView;
        
        public BuildingSelectionPresenter(BuildingSelectionView view, BuildingSelectionModel model, World world)
        {
            _model = model;
            _world = world;
            _view = view;
        }

        public void Enable()
        {
            _world.PlayerControls.Player.Click.performed += OnClick;
        }

        public void Disable()
        {
            _world.PlayerControls.Player.Click.performed -= OnClick;
        }

        private void OnClick(InputAction.CallbackContext context)
        {
            if (!_model.CanSelect)
            {
                return;
            }
            
            var ray = _view.Camera.ScreenPointToRay(_world.PlayerControls.Player.PointerPosition.ReadValue<Vector2>());
            
            var success = Physics.Raycast(ray, out var hitInfo);

            if (_cachedBuildingView != null)
            {
                _cachedBuildingView.SelectedOutline = false;
                    
                _model.ClearSelectedBuilding();
            }
            
            if (!success || !hitInfo.collider.TryGetComponent(out BuildingView foundedBuildingView))
            {
                _cachedBuildingView = null;
                
                return;
            }
            
            _cachedBuildingView = foundedBuildingView;
            
            _cachedBuildingView.SelectedOutline = true;
            
            _model.SelectBuilding(_cachedBuildingView.Id);
        }
    }
}