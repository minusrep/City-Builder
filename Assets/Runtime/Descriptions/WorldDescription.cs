using Runtime.Descriptions.Buildings;
using Runtime.Descriptions.Citizens;
using System.Collections.Generic;
using Runtime.Descriptions.Items;
using Runtime.Extensions;
using Runtime.ModelCollections;

namespace Runtime.Descriptions
{
    public sealed class WorldDescription : IDeserializeModel
    {
        public BuildingsDescriptionCollection BuildingCollection { get; private set; }
        
        public ResourceDescriptionCollection ResourceCollection { get; private set; }
        
        public CitizensDescription Citizens { get; private set; }
  
        public PointOfInterestDescriptionCollection PointOfInterestCollection { get; }
        
        private DescriptionFactory Factory { get; set; }
        
        public void Deserialize(Dictionary<string, object> data)
        {
            Factory = new DescriptionFactory();
            Factory.Register<ProductionBuildingDescription>("production");
            Factory.Register<ServiceBuildingDescription>("service");
            Factory.Register<DecorBuildingDescription>("decor");
            Factory.Register<StorageBuildingDescription>("storage");
            
            BuildingCollection = new BuildingsDescriptionCollection(data.GetNode("buildings"), Factory);
            ResourceCollection = new ResourceDescriptionCollection(data.GetNode("resources"));
            Citizens = new CitizensDescription(data.GetNode("citizens"));
        }
    }
}