using Runtime.Colony.Buildings.Models;
using Runtime.Colony.GameResources;

namespace Runtime
{
    public class FactoryProvider
    {
        public ResourceFactory ResourceFactory { get; }
        
        public BuildingFactory BuildingFactory { get; }

        public FactoryProvider(ResourceFactory resourceFactory, BuildingFactory buildingFactory)
        {
            BuildingFactory = buildingFactory;
            
            ResourceFactory = resourceFactory;
        }
    }
}