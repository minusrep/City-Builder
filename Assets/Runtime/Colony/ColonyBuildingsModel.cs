using System.Collections.Generic;
using Runtime.Colony.Buildings;

namespace Runtime.Colony
{
    public class ColonyBuildingsModel
    {
        public List<ProductionBuildingModel> ProductionBuildings { get; private set; }
        public List<ServiceBuildingModel> ServiceBuildings { get; private set; }
        public List<StorageBuildingModel> StorageBuildings { get; private set; }
        public List<DecorBuildingModel> DecorBuildings { get; private set; }
    }
}