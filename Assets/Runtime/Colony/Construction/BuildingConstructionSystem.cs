using System.Linq;
using Runtime.GameSystems;
using UnityEngine;

namespace Runtime.Colony.Construction
{
    public class BuildingConstructionSystem : IGameSystem
    {
        public string Id => "building_construction";

        private readonly BuildingConstructionModel _model;
        private readonly BuildingConstructionView _view;
        private readonly World _world;
        private readonly Plane _groundPlane = new(Vector3.up, Vector3.zero);

        public BuildingConstructionSystem(BuildingConstructionModel model, BuildingConstructionView view, World world)
        {
            _model = model;
            _view = view;
            _world = world;
        }

        public void Update(float deltaTime)
        {
            if (_model.SelectedBuilding != null)
            {
                var ray = _world.MainCamera.ScreenPointToRay(_model.CursorPosition);

                if (_groundPlane.Raycast(ray, out var distance))
                {
                    var worldPosition = ray.GetPoint(distance);
                    var gridPosition = _world.Grid.WorldToGrid(worldPosition);

                    var canPlace = _world.Grid.CanPlaceBuilding(
                        _model.SelectedBuilding,
                        gridPosition);

                    var previewWorldPosition = _world.Grid.GridToWorld(
                        gridPosition);

                    _model.CurrentGridPosition = gridPosition;
                    _model.CurrentWorldPosition = previewWorldPosition;
                    _model.CanPlace = canPlace;

                    _view.Transform.position = previewWorldPosition + _model.VisualWorldOffset;
                    _view.SetValid(canPlace);
                }

                if (_world.PlayerControls.Construction.Build.WasPressedThisFrame())
                {
                    TryPlaceBuilding();
                }
            }
        }

        private void TryPlaceBuilding()
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