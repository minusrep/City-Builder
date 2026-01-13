using Runtime.GameSystems;
using UnityEngine;

namespace Runtime.Colony.Buildings.Construction
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
                    _model.CanPlace = canPlace;

                    _view.Transform.position = previewWorldPosition;
                    _view.SetValid(canPlace);
                }
            }
        }
    }
}