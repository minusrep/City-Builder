using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Descriptions
{
    public class WorldGridDescription
    {
        public int Width { get; }
        public int Height { get; }
        public float CellSize { get; }

        public WorldGridDescription(Dictionary<string, object> data)
        {
            Width = data.GetInt("width");
            Height = data.GetInt("height");
            CellSize = data.GetFloat("cell_size");
        }
    }
}