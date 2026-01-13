using System.Linq;
using Runtime.Colony.Buildings.Common;
using Runtime.Common;
using Runtime.ViewDescriptions;
using Runtime.ViewDescriptions.Buildings;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Colony.Buildings.Construction
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
            _view.GameObject.SetActive(true);
            _model.OnChangeSelectedBuilding += RebuildView;
            _world.PlayerControls.Construction.Build.performed += TryPlaceBuilding;

            _world.GameSystems.Add(_system);
        }

        public void Disable()
        {
            _view.GameObject.SetActive(false);
            _model.OnChangeSelectedBuilding -= RebuildView;
            _world.PlayerControls.Construction.Build.performed -= TryPlaceBuilding;

            _world.GameSystems.Remove(_system);
        }

        private void RebuildView()
        {
            CleanupPreview();

            if (_model.SelectedBuilding != null)
            {
                var viewDescription = GetViewDescription();

                var previewInstance = Object.Instantiate(
                    viewDescription.Prefab.Preview,
                    _view.Transform,
                    false
                );

                _view.Preview = previewInstance;

                _view.Transform.localScale = BuildingVisualLayoutHelper.GetScale(viewDescription);
            }
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
        
        private void TryPlaceBuilding(InputAction.CallbackContext callbackContext)
        {
            if (_model.CanPlace)
            {
                _world.Buildings.Create(_model.SelectedBuilding.Id);
                var building = _world.Buildings.Models.Last().Value;

                _world.Grid.PlaceBuilding(building, _model.CurrentGridPosition);
            }
        }
    }
}