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
        
        public BuildingConstructionMenuPresenter(BuildingConstructionMenuView view,
            BuildingConstructionView constructionView,
            WorldDescription descriptions,
            World world, WorldViewDescriptions viewDescriptions, MenuContent menuContent)
        {
            _descriptions = descriptions;
            _menuContent = menuContent;
            _view = view;
            _constructionPresenter =
                new BuildingConstructionPresenter(constructionView, world, viewDescriptions);
        }

        public void Enable()
        {
            _menuContent.MenuRoot.Add(_view.Root);
            Show();
            BuildButtons();
        }

        public void Disable()
        {
            StopConstruction();
            Hide();
            _view.Root.Clear();
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
            StopConstruction();
            UpdateSelection(description.Id);
            _constructionPresenter.Model.SelectedBuilding = description;
            _constructionPresenter.Enable();
        }

        private void StopConstruction()
        {
            _constructionPresenter.Disable();

            ClearSelection();
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
                name = id
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
        
        private void Show() => _view.Root.style.display = DisplayStyle.Flex;
        private void Hide() => _view.Root.style.display = DisplayStyle.None;
    }
}