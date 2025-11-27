using System;

namespace Runtime.Colony.Buildings.Descriptions
{
    [Serializable]
    public class ServiceBuildingDescription : BuildingDescription
    {
        public long ServiceTime;
        public int MaxQueue;
        public int MaxCitizenAmount;
        public string ServiceResource;
    }
}