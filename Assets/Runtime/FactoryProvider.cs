using Runtime.Colony.Buildings.Common.Factories;
using Runtime.Colony.GameResources;

namespace Runtime
{
    public class FactoryProvider
    {
        public ResourceFactory ResourceFactory { get; }
        
        public BuildingModelFactory BuildingModelFactory { get; }

        public FactoryProvider(ResourceFactory resourceFactory, BuildingModelFactory buildingModelFactory)
        {
            BuildingModelFactory = buildingModelFactory;
            
            ResourceFactory = resourceFactory;
        }
    }
}