using System.Collections.Generic;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Descriptions.Buildings
{
    public abstract class BuildingDescription
    {
        public string Id { get; }
        public string Type { get; }
        public string ViewDescriptionId { get; }
        public Vector2Int Size { get; }
        public List<Vector2Int> Cells { get; } = new();

        protected BuildingDescription(string id, Dictionary<string, object> data)
        {
            Id = id;
            Type = data.GetString("type");
            ViewDescriptionId = data.GetString("view_id");
            Size = data.GetVector2Int("size");

            for (var x = 0; x < Size.x; x++)
            {
                for (var y = 0; y < Size.y; y++)
                {
                    Cells.Add(new Vector2Int(x, y));
                }
            }
        }
    }
}