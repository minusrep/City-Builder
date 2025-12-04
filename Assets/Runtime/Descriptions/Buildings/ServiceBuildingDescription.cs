using System;
using System.Collections.Generic;

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
            ServiceTime = (long)data["max_time_service"];
            MaxQueue = Convert.ToInt32(data["max_queue"]);
            MaxCitizenAmount = Convert.ToInt32(data["max_citizen_amount"]);
            ServiceResource = (string)data["service_resource"];
        }
    }
}