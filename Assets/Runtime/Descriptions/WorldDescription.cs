using Runtime.Descriptions.Buildings;
using Runtime.Descriptions.CameraControl;
using Runtime.Descriptions.Citizens;
using Runtime.Descriptions.Items;
using Runtime.Extensions;
using System.Collections.Generic;

namespace Runtime.Descriptions
{
    public sealed class WorldDescription
    {
        public BuildingsDescriptionCollection BuildingCollection { get; private set; }
        
        public ResourceDescriptionCollection ResourceCollection { get; private set; }
        
        public CitizensDescription Citizens { get; private set; }

        public CameraControlDescription CameraControlDescription { get; private set; }

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
            CameraControlDescription = new CameraControlDescription(data.GetNode("camera_control"));
            PointOfInterestCollection = new PointOfInterestDescriptionCollection(data.GetNode("points_of_interest"));
        }
    }
}