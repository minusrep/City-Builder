using Runtime.Common;
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
        private readonly BuildingViewDescriptionCollection _viewDescriptionCollection;
        private readonly BuildingConstructionSystem _system;

        public BuildingConstructionPresenter(BuildingConstructionModel model, BuildingConstructionView view,
            World world,
            WorldViewDescriptions viewDescriptions)
        {
            _world = world;
            _model = model;
            _view = view;
            _viewDescriptionCollection = viewDescriptions.BuildingViewDescriptions;
            _system = new BuildingConstructionSystem(_model, _view, world);
        }

        public void Enable()
        {
            _model.OnChangeSelectedBuilding += RebuildView;

            _world.GameSystems.Add(_system);
        }

        public void Disable()
        {
            _model.OnChangeSelectedBuilding -= RebuildView;
            _view.GameObject.SetActive(false);
            _world.GameSystems.Remove(_system);
        }

        private void RebuildView()
        {
            CleanupPreview();
            _view.GameObject.SetActive(true);

            var viewDescription = GetViewDescription();
            var previewRenderer = Object.Instantiate(viewDescription.Prefab.PreviewRenderer, _view.Transform, false);
            _model.VisualWorldOffset =
                BuildingVisualLayoutService.GetOffset(viewDescription, _world.Grid.Description.CellSize);

            _view.SetPreviewRenderer(previewRenderer);
            _view.Transform.localScale = BuildingVisualLayoutService.GetScale(viewDescription, previewRenderer.bounds,
                _world.Grid.Description.CellSize);
        }

        private void CleanupPreview()
        {
            _view.Transform.localScale = Vector3.one;
            if (_view.PreviewRenderer != null)
            {
                Object.Destroy(_view.PreviewRenderer.gameObject);
                _view.PreviewRenderer = null;
            }
        }

        private BuildingViewDescription GetViewDescription()
        {
            return _viewDescriptionCollection.Get(
                _model.SelectedBuilding.ViewDescriptionId
            );
        }
    }
}