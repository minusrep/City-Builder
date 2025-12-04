using System.Collections.Generic;
using Runtime.Utilities;

namespace Runtime.Descriptions.Buildings
{
    public abstract class BuildingDescription
    {
        public string Id { get; }
        public string Type { get; }

        protected BuildingDescription(string id, Dictionary<string, object> data)
        {
            Id = id;
            Type = data.GetString("type");
        }
    }
}