using Runtime.Descriptions.Buildings;
using Runtime.Descriptions.Citizens;
using System.Collections.Generic;
using Runtime.Descriptions.Items;
using Runtime.Extensions;

namespace Runtime.Descriptions
{
    public sealed class WorldDescription
    {
        public BuildingsDescriptionCollection BuildingCollection { get; private set; }
        
        public ResourceDescriptionCollection ResourceCollection { get; private set; }
        
        public CitizensDescription Citizens { get; private set; }
  
        public PointOfInterestDescriptionCollection PointOfInterestCollection { get; private set; }
        
        private DescriptionFactory Factory { get; set; }

        public void SetData(Dictionary<string, object> data)
        {
            Factory = new DescriptionFactory();
            Factory.Register<ProductionBuildingDescription>("production");
            Factory.Register<ServiceBuildingDescription>("service");
            Factory.Register<DecorBuildingDescription>("decor");
            Factory.Register<StorageBuildingDescription>("storage");
            
            BuildingCollection = new BuildingsDescriptionCollection(data.GetNode("buildings"), Factory);
            ResourceCollection = new ResourceDescriptionCollection(data.GetNode("resources"));
            Citizens = new CitizensDescription(data.GetNode("citizens"));
            PointOfInterestCollection = new PointOfInterestDescriptionCollection(data.GetNode("points_of_interest"));
        }
    }
}