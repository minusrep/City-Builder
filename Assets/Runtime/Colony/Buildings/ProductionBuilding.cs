using System;
using Runtime.Colony.Buildings.Descriptions;

namespace Runtime.Colony.Buildings
{
    [Serializable]
    public class ProductionBuilding : Building
    {
        public ProductionBuildingDescription Description;
        
        public ColonyOrdersPool OrderPool;
        public bool IsActive;
        public long CompleteProductionTime;
        public Resource Resource;

        public ProductionBuilding(ProductionBuildingDescription description)
        {
            Description = description;
        }
    }
}