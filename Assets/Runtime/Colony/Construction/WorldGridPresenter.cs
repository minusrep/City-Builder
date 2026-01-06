using System.Collections.Generic;
using Runtime.Common;
using UnityEngine;

namespace Runtime.Colony.Construction
{
    public class WorldGridPresenter : IPresenter
    {
        private readonly WorldGridModel _model;
        private readonly WorldGridView _view;

        public WorldGridPresenter(WorldGridModel model, WorldGridView view)
        {
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            _view.GameObject.SetActive(true);
            _view.Transform.position = new Vector3(_model.Description.Origin.x, 1f, _model.Description.Origin.z);
            BuildGridMesh();
        }

        public void Disable()
        {
            _view.GameObject.SetActive(false);
        }

        private void BuildGridMesh()
        {
            var mesh = Build(_model);
            _view.Filter.mesh = mesh;
        }

        private Mesh Build(WorldGridModel grid)
        {
            var mesh = new Mesh();
            var vertices = new List<Vector3>();
            var indices = new List<int>();

            var index = 0;
            for (var x = 0; x <= grid.Description.Width; x++)
            {
                vertices.Add(new Vector3(x * grid.Description.CellSize, 0, 0));
                vertices.Add(new Vector3(x * grid.Description.CellSize, 0,
                    grid.Description.Height * grid.Description.CellSize));
                indices.Add(index++);
                indices.Add(index++);
            }

            for (var y = 0; y <= grid.Description.Height; y++)
            {
                vertices.Add(new Vector3(0, 0, y * grid.Description.CellSize));
                vertices.Add(new Vector3(grid.Description.Width * grid.Description.CellSize, 0,
                    y * grid.Description.CellSize));
                indices.Add(index++);
                indices.Add(index++);
            }

            mesh.SetVertices(vertices);
            mesh.SetIndices(indices, MeshTopology.Lines, 0);
            return mesh;
        }
    }
}