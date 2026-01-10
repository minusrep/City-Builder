using System.Collections.Generic;
using Runtime.Extensions;

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

        protected BuildingDescription(string id, Dictionary<string, object> data)
        {
            Id = id;
            Type = data.GetString(TypeKey);
            ViewDescriptionId = data.GetString(ViewId);
            MaxLevel = data.GetInt(MaxLevelId);
        }
    }
}