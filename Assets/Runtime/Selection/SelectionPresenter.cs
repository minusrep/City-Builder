using Runtime.Colony;
using Runtime.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Selection
{
    public class SelectionPresenter : IPresenter
    {
        private readonly SelectionModel _model;
        
        private readonly World _world;

        private ISelectableUnit _cachedSelectable;

        private ISelectableUnit _cachedSelected;
        
        public SelectionPresenter(SelectionModel model, World world)
        {
            _model = model;
            _world = world;
        }

        public void Enable()
        {
            _world.PlayerControls.Player.Click.performed += OnClick;

            _world.PlayerControls.Player.PointerPosition.performed += OnChangePointerPosition;
        }

        public void Disable()
        {
            _world.PlayerControls.Player.Click.performed -= OnClick;
            
            _world.PlayerControls.Player.PointerPosition.performed -= OnChangePointerPosition;
        }

        private void OnChangePointerPosition(InputAction.CallbackContext context)
        {
            var ray = _world.MainCamera.ScreenPointToRay(context.ReadValue<Vector2>());

            if (!Physics.Raycast(ray, out var hitInfo) ||
                !hitInfo.collider.TryGetComponent(out ISelectableUnit selectable))
            {
                if (_cachedSelectable != null && _cachedSelectable != _cachedSelected)
                {
                    _cachedSelectable.Outline.SetOutline(SelectionOutlineType.None);
                }

                _cachedSelectable = null;
                return;
            }

            if (selectable == _cachedSelectable)
                return;

            if (_cachedSelectable != null && _cachedSelectable != _cachedSelected)
            {
                _cachedSelectable.Outline.SetOutline(SelectionOutlineType.None);
            }

            _cachedSelectable = selectable;

            if (_cachedSelectable != _cachedSelected)
            {
                _cachedSelectable.Outline.SetOutline(SelectionOutlineType.Hover);
            }
        }

        private void OnClick(InputAction.CallbackContext context)
        {
            if (!_model.CanSelect)
                return;

            if (_cachedSelected != null)
            {
                _cachedSelected.Outline.SetOutline(SelectionOutlineType.None);
            }

            if (_cachedSelectable == null)
            {
                _cachedSelected = null;
                _model.ClearSelected();
                return;
            }

            _cachedSelected = _cachedSelectable;
            _cachedSelected.Outline.SetOutline(SelectionOutlineType.Selected);
            _model.Select(_cachedSelected.Id);
        }
    }
}