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
        public IReadOnlyList<Vector2Int> Cells { get; }

        protected BuildingDescription(string id, Dictionary<string, object> data)
        {
            Id = id;
            Type = data.GetString("type");
            ViewDescriptionId = data.GetString("view_id");
            Size = data.GetVector2Int("size");
            Cells = data.GetList<Vector2Int>("cells");
        }
    }
}