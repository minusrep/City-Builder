using System.Collections.Generic;
using Runtime.Common;
using Runtime.Descriptions;
using Runtime.Descriptions.Buildings;
using Runtime.UI;
using Runtime.ViewDescriptions;
using UnityEngine.UIElements;

namespace Runtime.Colony.Construction.Menu
{
    public class BuildingConstructionMenuPresenter : IPresenter
    {
        private readonly BuildingConstructionMenuView _view;
        private readonly WorldDescription _descriptions;
        private readonly MenuContent _menuContent;
        private readonly Dictionary<string, Button> _buttons = new();
        private readonly BuildingConstructionPresenter _constructionPresenter;
        private readonly BuildingConstructionModel _constructionModel;
        
        public BuildingConstructionMenuPresenter(BuildingConstructionMenuView view,
            BuildingConstructionView constructionView,
            WorldDescription descriptions,
            World world, WorldViewDescriptions viewDescriptions, MenuContent menuContent)
        {
            _descriptions = descriptions;
            _menuContent = menuContent;
            _view = view;

            _constructionModel = new BuildingConstructionModel(world.PlayerControls);
            _constructionPresenter =
                new BuildingConstructionPresenter(_constructionModel, constructionView, world, viewDescriptions);
        }

        public void Enable()
        {
            _constructionPresenter.Enable();
            _menuContent.MenuRoot.Add(_view.Root);
            BuildButtons();
        }

        public void Disable()
        {
            _menuContent.MenuRoot.Remove(_view.Root);
            _view.Root.Clear();
            _constructionPresenter.Disable();
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
            UpdateSelection(description.Id);
            _constructionModel.SelectedBuilding = description;
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
    }
}