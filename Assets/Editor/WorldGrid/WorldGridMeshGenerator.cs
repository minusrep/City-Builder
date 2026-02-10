using System.Collections.Generic;
using UnityEngine;

namespace Editor.WorldGrid
{
    public static class WorldGridMeshGenerator
    {
        public static Mesh Generate(int width, int height, float cellSize)
        {
            var mesh = new Mesh { name = "SM_WorldGrid" };

            var vertices = new List<Vector3>();
            var indices = new List<int>();
            var index = 0;

            for (var x = 0; x <= width; x++)
            {
                vertices.Add(new Vector3(x * cellSize, 0, 0));
                vertices.Add(new Vector3(x * cellSize, 0, height * cellSize));
                indices.Add(index++);
                indices.Add(index++);
            }

            for (var y = 0; y <= height; y++)
            {
                vertices.Add(new Vector3(0, 0, y * cellSize));
                vertices.Add(new Vector3(width * cellSize, 0, y * cellSize));
                indices.Add(index++);
                indices.Add(index++);
            }

            mesh.SetVertices(vertices);
            mesh.SetIndices(indices, MeshTopology.Lines, 0);
            mesh.RecalculateBounds();

            return mesh;
        }
    }
}