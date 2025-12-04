using Runtime.Colony.Citizens;
using Runtime.Descriptions.Buildings;
using Runtime.Descriptions.GameResources;

namespace Runtime.Descriptions
{
    public sealed class Descriptions
    {
        public BuildingsDescriptionCollection BuildingDescriptionCollection { get; }
        public ResourceDescriptionCollection ResourceDescriptionCollection { get; }
        public CitizensDescription citizensDescription { get; }
    }
}