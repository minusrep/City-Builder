using Runtime.Colony;
using Runtime.Common;
using Runtime.Descriptions.Buildings;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.UI.HUD.BuildingHUD
{
    public class BuildingPanelPresenter : IPresenter
    {
        private const string BuildingInfoKey = "building-info";
        
        private const string BuildingInfoEnabledStyleKey = "building-info-enabled";
        
        private const string BuildingInfoTextFieldStyleKey = "building-info-field";
        
        private const string BuildingInfoTextTitleStyleKey = "building-info-title";
        
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
            _root = _view.Root.Q<VisualElement>(BuildingInfoKey);
            
            _model.OnChange += OnChange;
        }

        public void Disable()
        {
            _model.OnChange -= OnChange;
        }

        private void OnChange()
        {
            var notSelected = string.IsNullOrEmpty(_model.SelectedBuildingId);

            if (notSelected)
            {
                _root.RemoveFromClassList(BuildingInfoEnabledStyleKey);
                
                return;
            }
            
            var buildingModel = _world.Buildings.Get(_model.SelectedBuildingId);
            
            _root.AddToClassList(BuildingInfoEnabledStyleKey);
            
            _root.Clear();

            _root.Add(CreateTitle(buildingModel.BaseDescription.ViewDescriptionId));

            _root.Add(CreateField("Type: ", buildingModel.BaseDescription.Type));

            switch (buildingModel.BaseDescription)
            {
                case ServiceBuildingDescription serviceBuildingDescription:
                    _root.Add(CreateField("Resource: ", serviceBuildingDescription.ServiceResource));
                    break;
                
                case ProductionBuildingDescription productionBuildingDescription:
                    _root.Add(CreateField("Time: ", $"{productionBuildingDescription.ProductionResource}s"));
                    _root.Add(CreateField("Resource: ", productionBuildingDescription.ProductionResource));
                break;
            }
        }

        private TextElement CreateTitle(string value)
        {
            var title = CreateTextElement(BuildingInfoTextTitleStyleKey);

            title.text = value;
            
            return title;
        }
        
        private TextElement CreateField<T>(string name, T value)
        {
            var field = CreateTextElement(BuildingInfoTextFieldStyleKey);

            field.text = $"<color=#FFD700>{name}</color>{value.ToString()}";

            return field;
        }
        
        private TextElement CreateTextElement(string style)
        {
            var textElement = new TextElement();
            
            textElement.AddToClassList(style);
            
            return textElement;
        }
    }
}