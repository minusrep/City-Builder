using System;
using System.Collections.Generic;
using Runtime.Colony.Buildings.Descriptions;
using Runtime.Colony.GameResources;

namespace Runtime.Colony.Buildings
{
    [Serializable]
    public class StorageBuilding : Building
    {
        public StorageBuildingDescription Description;

        public Dictionary<string, ResourceModel> Resources = new();

        public StorageBuilding(StorageBuildingDescription desc)
        {
            Description = desc;
        }
    }
}