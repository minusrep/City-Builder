using Runtime.Colony.Buildings;
using Runtime.Colony.Items;

namespace Runtime
{
    public class FactoryProvider
    {
        public ItemFactory ItemFactory { get; }
        
        public BuildingFactory BuildingFactory { get; }

        public FactoryProvider(ItemFactory itemFactory, BuildingFactory buildingFactory)
        {
            BuildingFactory = buildingFactory;
            
            ItemFactory = itemFactory;
        }
    }
}