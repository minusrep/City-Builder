using System.Collections.Generic;
using Runtime.Colony.Buildings;

namespace Runtime.Colony
{
    public class ColonyBuildingsModel
    {
        public List<ProductionBuilding> ProductionBuildings { get; private set; }
        public List<ServiceBuilding> ServiceBuildings { get; private set; }
        public List<StorageBuilding> StorageBuildings { get; private set; }
        public List<DecorBuilding> DecorBuildings { get; private set; }
    }
}