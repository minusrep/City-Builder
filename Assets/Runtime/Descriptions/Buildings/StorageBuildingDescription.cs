using System;
using System.Collections.Generic;

namespace Runtime.Descriptions.Buildings
{
    public class StorageBuildingDescription : BuildingDescription
    {
        public int MaxResourceAmount { get; }
        public List<string> StoredResources { get; }

        public StorageBuildingDescription(Dictionary<string, object> data) : base(data)
        {
            MaxResourceAmount = Convert.ToInt32(data["max_resource_amount"]);
            
            StoredResources = new List<string>();
            
            foreach (var obj in (List<object>)data["stored_resources"])
            {
                StoredResources.Add((string)obj);
            }
        }
    }
}