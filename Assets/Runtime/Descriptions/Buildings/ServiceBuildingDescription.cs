using System;

namespace Runtime.Descriptions.Buildings
{
    public class ServiceBuildingDescription : BuildingDescription
    {
        public long ServiceTime;
        public int MaxQueue;
        public int MaxCitizenAmount;
        public string ServiceResource;
    }
}