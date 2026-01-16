using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Descriptions.Buildings
{
    public class ServiceBuildingDescription : BuildingDescription
    {
        public int MaxCitizenAmount { get; }
        public string ServiceResource { get; }

        public ServiceBuildingDescription(string id, Dictionary<string, object> data) : base(id, data)
        {
            MaxCitizenAmount = data.GetInt("max_citizen_amount");
            ServiceResource = data.GetString("service_resource");
        }
    }
}