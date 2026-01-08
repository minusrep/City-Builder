using Runtime.Common;
using Runtime.Descriptions;
using Runtime.Descriptions.Buildings;
using Runtime.GameSystems;
using Runtime.ViewDescriptions;
using Runtime.ViewDescriptions.Buildings;
using UnityEngine;

namespace Runtime.Colony.Construction
{
    public class BuildingConstructionPresenter : IPresenter
    {
        private readonly BuildingConstructionModel _model;
        private readonly BuildingConstructionView _view;
        private readonly World _world;
        private readonly GameSystemCollection _systemCollection;
        private readonly BuildingsDescriptionCollection _descriptionCollection;
        private readonly BuildingViewDescriptionCollection _viewDescriptionCollection;
        private readonly BuildingConstructionSystem _system;

        public BuildingConstructionPresenter(BuildingConstructionModel model, BuildingConstructionView view,
            World world,
            GameSystemCollection systemCollection,
            WorldDescription descriptions,
            WorldViewDescriptions viewDescriptions)
        {
            _model = model;
            _view = view;
            _world = world;
            _systemCollection = systemCollection;
            _descriptionCollection = descriptions.BuildingCollection;
            _viewDescriptionCollection = viewDescriptions.BuildingViewDescriptions;
            _system = new BuildingConstructionSystem(_model, _view, world);
        }

        public void Enable()
        {
            _model.IsActive = true;
            _model.SelectedBuilding = _descriptionCollection.Descriptions["sawmill"];

            SetupView();

            _systemCollection.Add(_system);
        }

        public void Disable()
        {
            _view.GameObject.SetActive(false);
            _systemCollection.Remove(_system);
        }

        private void SetupView()
        {
            _view.GameObject.SetActive(true);

            var viewDescription = GetViewDescription();
            var previewRenderer = Object.Instantiate(viewDescription.Prefab.PreviewRenderer);
            _model.VisualWorldOffset =
                BuildingVisualLayoutService.GetOffset(viewDescription, _world.Grid.Description.CellSize);

            _view.SetPreviewRenderer(previewRenderer);
            _view.Transform.localScale = BuildingVisualLayoutService.GetScale(viewDescription, previewRenderer.bounds,
                _world.Grid.Description.CellSize);
        }

        private BuildingViewDescription GetViewDescription()
        {
            return _viewDescriptionCollection.Get(
                _model.SelectedBuilding.ViewDescriptionId
            );
        }
    }
}