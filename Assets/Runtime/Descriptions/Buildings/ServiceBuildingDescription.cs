using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Descriptions.Buildings
{
    public class ServiceBuildingDescription : BuildingDescription
    {
        public long ServiceTime { get; }
        public int MaxQueue { get; }
        public int MaxCitizenAmount { get; }
        public string ServiceResource { get; }

        public ServiceBuildingDescription(string id, Dictionary<string, object> data) : base(id, data)
        {
            ServiceTime = data.GetLong("max_time_service");
            MaxQueue = data.GetInt("max_queue");
            MaxCitizenAmount = data.GetInt("max_citizen_amount");
            ServiceResource = data.GetString("service_resource");
        }
    }
}