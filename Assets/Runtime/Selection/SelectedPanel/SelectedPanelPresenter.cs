using Runtime.Colony;
using Runtime.Common;
using UnityEngine.UIElements;

namespace Runtime.Selection.SelectedPanel
{
    public class SelectedPanelPresenter : IPresenter
    {
        private readonly SelectedPanelView _view;
        private readonly SelectionModel _model;
        private readonly World _world;

        private SelectedBuildingPanelPresenter _selectedBuildingPanelPresenter;
        
        private SelectedCitizenPanelPresenter _selectedCitizenPanelPresenter;
        
        public SelectedPanelPresenter(SelectedPanelView view, SelectionModel model, World world)
        {
            _model = model;
            _view = view;
            _world = world;
        }

        public void Enable()
        {
            _selectedBuildingPanelPresenter = new SelectedBuildingPanelPresenter(_model, _view, _world);
            
            _selectedCitizenPanelPresenter = new SelectedCitizenPanelPresenter(_model, _view, _world);
            
            _selectedBuildingPanelPresenter.Enable();
            
            _selectedCitizenPanelPresenter.Enable();

            _model.OnChange += OnChange;
            
            TogglePanel();
            
            _view.Root.RegisterCallback<PointerEnterEvent>(OnPointerEnter);
            _view.Root.RegisterCallback<PointerLeaveEvent>(OnPointerLeave);
        }

        public void Disable()
        {
            _selectedBuildingPanelPresenter.Disable();

            _selectedCitizenPanelPresenter.Disable();

            _selectedBuildingPanelPresenter = null;

            _selectedCitizenPanelPresenter = null;
            
            _view.Root.UnregisterCallback<PointerEnterEvent>(OnPointerEnter);
            _view.Root.UnregisterCallback<PointerLeaveEvent>(OnPointerLeave);
            
            _model.OnChange -= OnChange;
        }

        private void OnChange()
        {
            TogglePanel();
        }

        private void TogglePanel()
        {
            if (string.IsNullOrEmpty(_model.SelectedId))
            {
                SelectionPanelUtility.HidePanel(_view.Root);
            }
        }

        private void OnPointerEnter(PointerEnterEvent evt)
        {
            _model.CanSelect = false;
        }

        private void OnPointerLeave(PointerLeaveEvent evt)
        {
            _model.CanSelect = true;
        }
    }
}