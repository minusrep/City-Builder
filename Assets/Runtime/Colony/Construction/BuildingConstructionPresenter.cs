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

            var previewInstance = Object.Instantiate(
                viewDescription.Prefab.Preview,
                _view.Transform,
                false
            );

            _view.Preview =  previewInstance;

            _model.VisualWorldOffset =
                BuildingVisualLayoutService.GetOffset(viewDescription, _world.Grid.Description.CellSize);

            _view.Transform.localScale = BuildingVisualLayoutService.GetScale(viewDescription, _view.Preview.Renderers,
                _world.Grid.Description.CellSize);
        }

        private void CleanupPreview()
        {
            _view.Transform.localScale = Vector3.one;

            if (_view.Preview != null)
            {
                Object.Destroy(_view.Preview.GameObject);
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