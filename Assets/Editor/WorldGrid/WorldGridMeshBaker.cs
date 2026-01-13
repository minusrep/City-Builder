#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Editor.WorldGrid
{
    public static class WorldGridMeshBaker
    {
        private const float CellSize = 5f;
        
        private const string ConfigPath = "Descriptions/world_grid_description";
        private const string Path = "Assets/Content/Meshes/SM_WorldGrid.mesh";

        [MenuItem("Tools/Grid/Bake World Grid Mesh")]
        public static void Bake()
        {
            var text = Resources.Load<TextAsset>(ConfigPath).text;
            var data = (Dictionary<string, object>)fastJSON.JSON.Parse(text);
            var width = Convert.ToInt32(data["width"]);
            var height = Convert.ToInt32(data["height"]);
            var mesh = WorldGridMeshGenerator.Generate(width, height, CellSize);

            AssetDatabase.CreateAsset(mesh, Path);
            AssetDatabase.SaveAssets();

            Selection.activeObject = mesh;
        }
    }
}
#endif