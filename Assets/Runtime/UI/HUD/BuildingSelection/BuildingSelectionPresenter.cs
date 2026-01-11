using Runtime.Colony.Buildings.Common;
using Runtime.Common;
using Runtime.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.UI.HUD
{
    public class BuildingSelectionPresenter : IPresenter
    {
        private readonly BuildingSelectionView _view;
        
        private readonly BuildingSelectionModel _model;
        
        private readonly PlayerControls _playerControls;

        private BuildingView _cachedBuildingView;
        
        public BuildingSelectionPresenter(BuildingSelectionView view, BuildingSelectionModel model, PlayerControls playerControls)
        {
            _playerControls = playerControls;
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _playerControls.Player.Click.performed += OnClick;
        }

        public void Disable()
        {
            _playerControls.Player.Click.performed -= OnClick;
        }

        private void OnClick(InputAction.CallbackContext context)
        {
            if (!_model.CanSelect)
            {
                return;
            }
            
            var ray = _view.Camera.ScreenPointToRay(_playerControls.Player.PointerPosition.ReadValue<Vector2>());
            
            var success = Physics.Raycast(ray, out RaycastHit hitInfo);

            BuildingView foundedBuildingView = null; 
            
            if (_cachedBuildingView != null)
            {
                _cachedBuildingView.SelectedOutline = false;
                    
                _model.ClearSelectedBuilding();
            }
            
            if (!success || !hitInfo.collider.TryGetComponent(out foundedBuildingView))
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