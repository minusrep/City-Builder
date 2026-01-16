using Runtime.Colony;
using Runtime.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Selection
{
    public class SelectionPresenter : IPresenter
    {
        private readonly SelectionModel _model;
        private readonly SelectionView _view;

        private readonly World _world;

        private ISelectableUnit _cachedSelectable;

        private ISelectableUnit _cachedSelected;
        
        public SelectionPresenter(SelectionModel model, SelectionView view, World world)
        {
            _model = model;
            _view = view;
            _world = world;
        }

        public void Enable()
        {
            _world.PlayerControls.Player.Click.performed += OnClick;

            _world.PlayerControls.Player.PointerPosition.performed += OnChangePointerPosition;
        }

        public void Disable()
        {
            _view.UnitCameraTransform.parent = _view.transform;
            
            _world.PlayerControls.Player.Click.performed -= OnClick;
            
            _world.PlayerControls.Player.PointerPosition.performed -= OnChangePointerPosition;
        }

        private void OnChangePointerPosition(InputAction.CallbackContext context)
        {
            if (_model.CanSelect)
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
                RenderIcon();
                return;
            }

            _cachedSelected = _cachedSelectable;
            _cachedSelected.Outline.SetOutline(SelectionOutlineType.Selected);
            _model.Select(_cachedSelected.Id);
            RenderIcon();
        }

        private void RenderIcon()
        {
            if (_cachedSelected == null)
            {
                _view.UnitCamera.enabled = false;

                return;
            }
            
            _view.UnitCamera.enabled = true;
            
            _view.UnitCameraTransform.parent = _cachedSelected.RenderPoint;
            
            _view.UnitCameraTransform.transform.position = _cachedSelected.RenderPoint.transform.position;
            
            _view.UnitCameraTransform.transform.rotation = _cachedSelected.RenderPoint.transform.rotation;
        }
    }
}