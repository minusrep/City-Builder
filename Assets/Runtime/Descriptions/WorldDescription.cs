using Runtime.Descriptions.GameResources;
using Runtime.Descriptions.Buildings;
using Runtime.Descriptions.Citizens;
using System.Collections.Generic;
using Runtime.Descriptions.StateMachine;
using Runtime.Extensions;

namespace Runtime.Descriptions
{
    public sealed class WorldDescription
    {
        public BuildingsDescriptionCollection BuildingCollection { get; }
        
        public ResourceDescriptionCollection ResourceCollection { get; }
        
        public CitizensDescription Citizens { get; }
  
        public PointOfInterestDescriptionCollection PointsOfInterest { get; }
        
        public StateDescriptionCollection States { get; }
        
        private DescriptionFactory Factory { get; }

        public WorldDescription(Dictionary<string, object> data)
        {
            Factory = new DescriptionFactory();
            Factory.Register<ProductionBuildingDescription>("production");
            Factory.Register<ServiceBuildingDescription>("service");
            Factory.Register<DecorBuildingDescription>("decor");
            Factory.Register<StorageBuildingDescription>("storage");
            
            BuildingCollection = new BuildingsDescriptionCollection(data.GetNode("buildings"), Factory);
            ResourceCollection = new ResourceDescriptionCollection(data.GetNode("resources"));
            Citizens = new CitizensDescription(data.GetNode("citizens"));
            PointsOfInterest = new PointOfInterestDescriptionCollection(data.GetNode("points_of_interest"));
            States = new StateDescriptionCollection(data.GetNode("states"));
        }
    }
}