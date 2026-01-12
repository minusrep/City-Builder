using System.Collections.Generic;
using Runtime.Common;
using Runtime.Descriptions;
using Runtime.Descriptions.Buildings;
using Runtime.UI;
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
        private readonly Dictionary<string, Button> _buttons = new();

        public BuildingConstructionMenuPresenter(BuildingConstructionMenuView view,
            World world,
            WorldDescription descriptions,
            MenuContent menuContent)
        {
            _descriptions = descriptions;
            _world = world;
            _menuContent = menuContent;
            _view = view;
        }

        public void Enable()
        {
            _world.PlayerControls.Construction.Cancel.performed += HandleCancelConstruction;

            _menuContent.HudLayer.Add(_view.Root);

            BuildButtons();
        }

        public void Disable()
        {
            _world.PlayerControls.Construction.Cancel.performed -= HandleCancelConstruction;

            _menuContent.HudLayer.Remove(_view.Root);

            ClearSelection();
            _buttons.Clear();
        }

        private void BuildButtons()
        {
            foreach (var description in _descriptions.BuildingCollection.Descriptions.Values)
            {
                var button = CreateBuildingButton(
                    description.Id,
                    description.ViewDescriptionId);

                button.clicked += () => StartConstruction(description);

                _view.BuildingList.Add(button);
                _buttons[description.Id] = button;
            }
        }

        private void StartConstruction(BuildingDescription description)
        {
            _world.PlayerControls.UI.Disable();
            _world.PlayerControls.Construction.Enable();

            UpdateSelection(description.Id);
            _world.BuildingConstructionModel.SelectedBuilding = description;
            _world.Grid.IsActive = true;
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

        private Button CreateBuildingButton(string id, string title)
        {
            var button = new Button
            {
                text = title,
                name = id,
                focusable = false
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
            _world.PlayerControls.UI.Enable();
            _world.PlayerControls.Construction.Disable();
            _world.BuildingConstructionModel.SelectedBuilding = null;
            _world.Grid.IsActive = false;
            ClearSelection();
        }
    }
}