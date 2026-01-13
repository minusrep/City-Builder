using System.Collections.Generic;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Descriptions
{
    public class WorldGridDescription
    {
        public const float CellSize = 5f;
        public int Width { get; }
        public int Height { get; }
        public Vector3 Origin { get; }

        public WorldGridDescription(Dictionary<string, object> data)
        {
            Width = data.GetInt("width");
            Height = data.GetInt("height");
            Origin = data.GetVector3("origin_position");
        }
    }
}