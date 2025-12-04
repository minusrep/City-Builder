using Runtime.Descriptions.GameResources;
using Runtime.Descriptions.Buildings;
using System.Collections.Generic;
using Runtime.Colony.Citizens;
using Runtime.Utilities;

namespace Runtime.Descriptions
{
    public sealed class Descriptions
    {
        public BuildingsDescriptionCollection BuildingDescriptionCollection { get; }
        
        public ResourceDescriptionCollection ResourceDescriptionCollection { get; }
        
        public CitizensDescription CitizensDescription { get; }
        
        private DescriptionFactory Factory { get; }

        public Descriptions(Dictionary<string, object> data)
        {
            Factory = new DescriptionFactory();
            Factory.Register<ProductionBuildingDescription>("production");
            Factory.Register<ServiceBuildingDescription>("service");
            Factory.Register<DecorBuildingDescription>("decor");
            Factory.Register<StorageBuildingDescription>("storage");
            
            
            BuildingDescriptionCollection = new BuildingsDescriptionCollection(data.GetDict("buildings"), Factory);
            ResourceDescriptionCollection = new ResourceDescriptionCollection(data.GetDict("resources"));
            CitizensDescription = new CitizensDescription();
        }
    }
}