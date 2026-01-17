using System;
using System.Collections.Generic;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Descriptions.Buildings
{
    public abstract class BuildingDescription
    {
        private const string TypeKey = "type";
        private const string ViewId = "view_id";
        private const string MaxLevelId = "max_level";
        
        public string Id { get; }
        public string Type { get; }
        public string ViewDescriptionId { get; }
        public int MaxLevel { get; }
        public Vector2Int Size { get; }
        public List<Vector2Int> Cells { get; } = new();
        public List<Vector3> InteractionPoints { get; } = new();

        protected BuildingDescription(string id, Dictionary<string, object> data)
        {
            Id = id;
            Type = data.GetString(TypeKey);
            ViewDescriptionId = data.GetString(ViewId);
            MaxLevel = data.GetInt(MaxLevelId);
            Size = data.GetVector2Int("size");
            
            foreach (var list in data.GetList<List<object>>("interaction_points"))
            {
                InteractionPoints.Add(new Vector3(
                    Convert.ToSingle(list[0]),
                    Convert.ToSingle(list[1]),
                    Convert.ToSingle(list[2])
                ) * WorldGridDescription.CellSize);
            }

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