using System.Collections.Generic;
using Runtime.Colony.Buildings.Descriptions;

namespace Runtime.Colony.Buildings
{
    public class ServiceBuilding : Building
    {
        public ServiceBuildingDescription Description;
        
        public bool IsActive;
        public Dictionary<string, long> InService = new();
        public Queue<int> WaitingQueue = new();

        public ServiceBuilding(ServiceBuildingDescription description)
        {
            Description = description;
        }
    }
}