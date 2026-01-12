using Runtime.Colony;
using Runtime.Colony.Buildings.Production;
using Runtime.Colony.Buildings.Service;
using Runtime.Common;
using Runtime.UI.HUD.BuildingSelection;
using UnityEngine.UIElements;

namespace Runtime.UI.HUD.BuildingPanel
{
    public class BuildingPanelPresenter : IPresenter
    {
        private const string BuildingPanelKey = "building-panel";
        
        private const string BuildingPanelEnabledStyleKey = "building-panel-enabled";
        
        private const string BuildingInfoPanelFieldStyleKey = "building-panel-field";
        
        private const string BuildingPanelTextTitleStyleKey = "building-panel-title";
        
        private const string BuildingInfoUpgradeButtonStyleKey = "building-panel-upgrade-button";
        
        private readonly HUDView _view;

        private readonly BuildingSelectionModel _model;

        private readonly World _world;

        private VisualElement _root;


        public BuildingPanelPresenter(HUDView view, BuildingSelectionModel model, World world)
        {
            _model = model;
            _view = view;
            _world = world;
        }

        public void Enable()
        {
            _root = _view.Root.Q<VisualElement>(BuildingPanelKey);
            
            _root.RegisterCallback<PointerEnterEvent>(OnPointerEnter);
            _root.RegisterCallback<PointerLeaveEvent>(OnPointerLeave);
            
            _model.OnChange += OnChange;
        }

        public void Disable()
        {
            _model.OnChange -= OnChange;
        }

        private void OnChange()
        {
            TogglePanel();
        }

        private void TogglePanel()
        {
            var notSelected = string.IsNullOrEmpty(_model.SelectedBuildingId);
            
            if (notSelected)
            {
                _root.RemoveFromClassList(BuildingPanelEnabledStyleKey);

                return;
            }
            
            var buildingModel = _world.Buildings.Get(_model.SelectedBuildingId);
            
            _root.AddToClassList(BuildingPanelEnabledStyleKey);
            
            _root.Clear();

            _root.Add(CreateTitle(buildingModel.BaseDescription.ViewDescriptionId));

            _root.Add(CreateField("Type: ", buildingModel.BaseDescription.Type));
            _root.Add(CreateField("Level: ", buildingModel.Level + 1));

            switch (buildingModel)
            {
                case ServiceBuildingModel service:
                    _root.Add(CreateField("Resource: ", service.Description.ServiceResource));
                    break;
                case ProductionBuildingModel production:
                    _root.Add(CreateField("Time: ", $"{production.ProductionTime / 1000f}s"));
                    _root.Add(CreateField("Resource: ", production.Description.ProductionResource));
                    break;
            }

            if (buildingModel.CanUpgrade)
            {
                var upgradeButton = new Button()
                {
                    enableRichText = true,
                    text = $"Upgrade to <color=#FFD700>{buildingModel.Level + 2}"
                };

                upgradeButton.clicked += () =>
                {
                    buildingModel.Upgrade();
                    
                    TogglePanel();
                };
                
                upgradeButton.AddToClassList(BuildingInfoUpgradeButtonStyleKey);
                
                _root.Add(upgradeButton);
            }
        }

        private TextElement CreateTitle(string value)
        {
            var title = CreateTextElement(BuildingPanelTextTitleStyleKey);

            title.text = value;
            
            return title;
        }

        private TextElement CreateField<T>(string name, T value)
        {
            var field = CreateTextElement(BuildingInfoPanelFieldStyleKey);

            field.text = $"<color=#FFD700>{name}</color>{value.ToString()}";

            return field;
        }

        private TextElement CreateTextElement(string style)
        {
            var textElement = new TextElement();
            
            textElement.AddToClassList(style);
            
            return textElement;
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