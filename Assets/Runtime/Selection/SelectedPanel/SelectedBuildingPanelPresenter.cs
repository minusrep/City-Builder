using Runtime.Colony.Buildings.Production;
using Runtime.Colony.Buildings.Service;
using Runtime.Common;

namespace Runtime.Colony.Buildings.Selection.BuildingPanel
{
    public class SelectedBuildingPanelPresenter : IPresenter
    {
        
        private readonly SelectedPanelView _view;
        private readonly SelectionModel _model;
        private readonly World _world;

        public SelectedBuildingPanelPresenter(SelectedPanelView view, SelectionModel model, World world)
        {
            _view = view;
            _model = model;
            _world = world;
        }
        
        public void Enable()
        {
            _model.OnChange += OnChange;
        }

        public void Disable()
        {
            _model.OnChange -= OnChange;
        }

        private void OnChange()
        {
            TryDrawBuildingModel();
        }

        private void TryDrawBuildingModel()
        {
            if (!_world.Buildings.TryGet(_model.SelectedId, out var selectedBuilding))
            {
                return;
            }
            
            SelectionPanelUtility.SetupPanel(_view.Root);

            _view.Root.Add(SelectionPanelUtility.CreateTitle(selectedBuilding.BaseDescription.ViewDescriptionId));

            _view.Root.Add(SelectionPanelUtility.CreateField("Type: ", selectedBuilding.BaseDescription.Type));
            _view.Root.Add(SelectionPanelUtility.CreateField("Level: ", selectedBuilding.Level + 1));

            switch (selectedBuilding)
            {
                case ServiceBuildingModel service:
                    _view.Root.Add(SelectionPanelUtility.CreateField("Resource: ", service.Description.ServiceResource));
                    break;
                case ProductionBuildingModel production:
                    _view.Root.Add(SelectionPanelUtility.CreateField("Time: ", $"{production.ProductionTime / 1000f}s"));
                    _view.Root.Add(SelectionPanelUtility.CreateField("Resource: ", production.Description.ProductionResource));
                    break;
            }

            if (selectedBuilding.CanUpgrade)
            {
                _view.Root.Add(SelectionPanelUtility.CreateButton($"Upgrade to ", selectedBuilding.Level + 2, () =>
                {
                    selectedBuilding.Upgrade();
                    
                    TryDrawBuildingModel();
                }));
            }
        }
    }
}