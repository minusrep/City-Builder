using System;
using System.Collections.Generic;
using Mono.Cecil;
using Runtime.Colony.Buildings.Descriptions;

namespace Runtime.Colony.Buildings
{
    [Serializable]
    public class StorageBuilding : Building
    {
        public StorageBuildingDescription Description;

        public Dictionary<string, Resource> Resources = new();

        public StorageBuilding(StorageBuildingDescription desc)
        {
            Description = desc;
        }
    }
}