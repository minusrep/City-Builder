using Runtime.Colony.Buildings.Production;
using Runtime.Colony.Buildings.Service;
using Runtime.Common;
using UnityEngine.UIElements;

namespace Runtime.Colony.Buildings.Selection.BuildingPanel
{
    public class BuildingPanelPresenter : IPresenter
    {
        private const string BuildingPanelEnabledStyleKey = "building-panel-enabled";
        
        private const string BuildingInfoPanelFieldStyleKey = "building-panel-field";
        
        private const string BuildingPanelTextTitleStyleKey = "building-panel-title";
        
        private const string BuildingInfoUpgradeButtonStyleKey = "building-panel-upgrade-button";
        
        private readonly BuildingPanelView _view;

        private readonly BuildingSelectionModel _model;

        private readonly World _world;
        
        public BuildingPanelPresenter(BuildingSelectionModel model, BuildingPanelView view, World world)
        {
            _model = model;
            _view = view;
            _world = world;
        }

        public void Enable()
        {
            _view.Root.RegisterCallback<PointerEnterEvent>(OnPointerEnter);
            _view.Root.RegisterCallback<PointerLeaveEvent>(OnPointerLeave);
            
            _model.OnChange += OnChange;
        }

        public void Disable()
        {
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
            var notSelected = string.IsNullOrEmpty(_model.SelectedBuildingId);
            
            if (notSelected)
            {
                _view.Root.RemoveFromClassList(BuildingPanelEnabledStyleKey);

                return;
            }
            
            var buildingModel = _world.Buildings.Get(_model.SelectedBuildingId);
            
            _view.Root.AddToClassList(BuildingPanelEnabledStyleKey);
            
            _view.Root.Clear();

            _view.Root.Add(CreateTitle(buildingModel.BaseDescription.ViewDescriptionId));

            _view.Root.Add(CreateField("Type: ", buildingModel.BaseDescription.Type));
            _view.Root.Add(CreateField("Level: ", buildingModel.Level + 1));

            switch (buildingModel)
            {
                case ServiceBuildingModel service:
                    _view.Root.Add(CreateField("Resource: ", service.Description.ServiceResource));
                    break;
                case ProductionBuildingModel production:
                    _view.Root.Add(CreateField("Time: ", $"{production.ProductionTime / 1000f}s"));
                    _view.Root.Add(CreateField("Resource: ", production.Description.ProductionResource));
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
                
                _view.Root.Add(upgradeButton);
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