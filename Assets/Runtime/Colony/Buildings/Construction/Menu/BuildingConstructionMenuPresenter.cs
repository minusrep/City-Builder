using System.Collections.Generic;
using Runtime.Common;
using Runtime.Descriptions;
using Runtime.Descriptions.Buildings;
using Runtime.UI;
using Runtime.ViewDescriptions;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Runtime.Colony.Buildings.Construction.Menu
{
    public class BuildingConstructionMenuPresenter : IPresenter
    {
        private readonly BuildingConstructionMenuView _view;
        private readonly WorldDescription _descriptions;
        private readonly World _world;
        private readonly MenuContent _menuContent;
        private readonly WorldViewDescriptions _worldViewDescriptions;
        private readonly Dictionary<string, Button> _buttons = new();
        
        private bool _isConstructionModeEnabled;

        public BuildingConstructionMenuPresenter(BuildingConstructionMenuView view,
            World world,
            WorldDescription descriptions,
            MenuContent menuContent, WorldViewDescriptions worldViewDescriptions)
        {
            _descriptions = descriptions;
            _world = world;
            _menuContent = menuContent;
            _worldViewDescriptions = worldViewDescriptions;
            _view = view;
        }

        public void Enable()
        {
            _world.PlayerControls.Construction.Cancel.performed += HandleCancelConstruction;

            _menuContent.HudLayer.Add(_view.Root);
            
            _view.Root.RegisterCallback<PointerEnterEvent>(OnPointerEnter);
            _view.Root.RegisterCallback<PointerLeaveEvent>(OnPointerLeave);
            
            _view.ToggleButton.clicked += HandleToggleConstructionMode;

            BuildButtons();
        }

        public void Disable()
        {
            _world.PlayerControls.Construction.Cancel.performed -= HandleCancelConstruction;

            _menuContent.HudLayer.Remove(_view.Root);
            
            _view.Root.UnregisterCallback<PointerEnterEvent>(OnPointerEnter);
            _view.Root.UnregisterCallback<PointerLeaveEvent>(OnPointerLeave);

            ClearSelection();
            _buttons.Clear();
        }

        private void BuildButtons()
        {
            foreach (var description in _descriptions.BuildingCollection.Descriptions.Values)
            {
                var button = CreateBuildingButton(description.ViewDescriptionId);

                button.clicked += () => StartConstruction(description);

                _view.BuildingList.Add(button);
                _buttons[description.Id] = button;
            }
        }

        private void StartConstruction(BuildingDescription description)
        {
            _world.SelectionModel.CanSelect = false;
            _world.PlayerControls.UI.Disable();
            _world.PlayerControls.Construction.Enable();
            
            foreach (var buildingModel in _world.Buildings.Models.Values)
            {
                buildingModel.SetConstructionMode(true);
            }

            UpdateSelection(description.Id);
            _world.BuildingConstructionModel.SelectedBuilding = description;
        }

        private void UpdateSelection(string selectedId)
        {
            foreach (var (id, button) in _buttons)
            {
                SetSelectedButton(button, id == selectedId);
            }
        }

        private void ClearSelection()
        {
            foreach (var button in _buttons.Values)
            {
                SetSelectedButton(button, false);
            }
        }

        private Button CreateBuildingButton(string title)
        {
            var viewDescription = _worldViewDescriptions.BuildingViewDescriptions.Get(title);
            
            var button = new Button
            {
                text = viewDescription.Title,
                name = title,
                focusable = false,
                style = { backgroundImage = viewDescription.Icon.texture}
            };

            button.AddToClassList("building-button");
            return button;
        }

        private void SetSelectedButton(Button button, bool selected)
        {
            if (selected)
                button.AddToClassList("building-button--selected");
            else
                button.RemoveFromClassList("building-button--selected");
        }

        private void HandleCancelConstruction(InputAction.CallbackContext obj)
        {
            _world.SelectionModel.CanSelect = true;
            _world.PlayerControls.UI.Enable();
            _world.PlayerControls.Construction.Disable();
            _world.BuildingConstructionModel.SelectedBuilding = null;
            
            foreach (var buildingModel in _world.Buildings.Models.Values)
            {
                buildingModel.SetConstructionMode(false);
            }
            
            ClearSelection();
        }
        
        private void HandleToggleConstructionMode()
        {
            _isConstructionModeEnabled = !_isConstructionModeEnabled;
            _view.ConstructionPanel.style.display = _isConstructionModeEnabled ? DisplayStyle.Flex : DisplayStyle.None;
            _view.ToggleButton.text = _isConstructionModeEnabled ? ">" : "<";
            _world.Grid.IsActive = _isConstructionModeEnabled;
        }

        private void OnPointerEnter(PointerEnterEvent evt)
        {
            _world.MainCameraControl.IsZooming = false;
        }

        private void OnPointerLeave(PointerLeaveEvent evt)
        {
            _world.MainCameraControl.IsZooming = true;
        }
    }
}