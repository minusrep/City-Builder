using Runtime.Colony.Buildings.Descriptions;
using Runtime.Colony.Orders;
using System;
using Runtime.Colony.GameResources;

namespace Runtime.Colony.Buildings
{
    [Serializable]
    public class ProductionBuilding : Building
    {
        public ProductionBuildingDescription Description;
        
        public ColonyOrdersPool OrderPool;
        public bool IsActive;
        public long CompleteProductionTime;
        public ResourceModel Resource;

        public ProductionBuilding(ProductionBuildingDescription description)
        {
            Description = description;
        }
    }
}