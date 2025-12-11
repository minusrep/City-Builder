using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Descriptions.Buildings
{
    public class StorageBuildingDescription : BuildingDescription
    {
        public int MaxResourceAmount { get; }
        public List<string> StoredResources { get; }

        public StorageBuildingDescription(string id, Dictionary<string, object> data) : base(id, data)
        {
            MaxResourceAmount = data.GetInt("max_resource_amount");
            
            StoredResources = new List<string>();
            
            foreach (var resource in data.GetList<string>("stored_resources"))
            {
                StoredResources.Add(resource);
            }
        }
    }
}