using System;
using Mono.Cecil;
using Runtime.Colony.Buildings.Descriptions;
using Runtime.Colony.Orders;

namespace Runtime.Colony.Buildings
{
    [Serializable]
    public class ProductionBuilding : Building
    {
        public ProductionBuildingDescription Description;
        
        public ColonyOrdersManager OrderManager;
        public bool IsActive;
        public long CompleteProductionTime;
        public Resource Resource;

        public ProductionBuilding(ProductionBuildingDescription description)
        {
            Description = description;
        }
    }
}