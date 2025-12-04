using System.Collections.Generic;

namespace Runtime.Descriptions.Buildings
{
    public abstract class BuildingDescription
    {
        public string Id { get; }
        public string Type { get; }

        protected BuildingDescription(string id, Dictionary<string, object> data)
        {
            Id = id;
            Type = (string)data["type"];
        }
    }
}