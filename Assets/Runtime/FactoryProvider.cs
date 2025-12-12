using Runtime.Colony.Buildings;
using Runtime.Colony.Buildings.Common;
using Runtime.Colony.Buildings.Common.Factories;
using Runtime.Colony.Items;

namespace Runtime
{
    public class FactoryProvider
    {
        public ItemFactory ItemFactory { get; }
        
        public BuildingModelFactory BuildingModelFactory { get; }

        public FactoryProvider(ItemFactory itemFactory, BuildingModelFactory buildingModelFactory)
        {
            BuildingModelFactory = buildingModelFactory;
            
            ItemFactory = itemFactory;
        }
    }
}