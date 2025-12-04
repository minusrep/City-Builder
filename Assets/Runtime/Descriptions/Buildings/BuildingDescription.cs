using System.Collections.Generic;

namespace Runtime.Descriptions.Buildings
{
    public abstract class BuildingDescription
    {
        public string Type { get; }

        protected BuildingDescription(Dictionary<string, object> data)
        {
            Type = (string)data["type"];
        }
    }
}