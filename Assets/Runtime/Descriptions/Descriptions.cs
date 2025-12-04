using System.Collections.Generic;
using Runtime.Descriptions.Buildings;
using Runtime.Descriptions.GameResources;

namespace Runtime.Descriptions
{
    public sealed class Descriptions
    {
        public BuildingsDescriptionCollection BuildingDescriptionCollection { get; }
        public ResourceDescriptionCollection  ResourceDescriptionCollection { get; }
 
        public Descriptions(Dictionary<string, object> descriptions, DescriptionFactory factory)
        {
            BuildingDescriptionCollection =
                new BuildingsDescriptionCollection((Dictionary<string, object>)descriptions["buildings"], factory);

            ResourceDescriptionCollection = new ResourceDescriptionCollection((Dictionary<string, object>)descriptions["resources"]);
        }
    }
}