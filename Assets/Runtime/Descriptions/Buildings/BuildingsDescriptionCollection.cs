using System.Collections.Generic;

namespace Runtime.Descriptions.Buildings
{
    public sealed class BuildingsDescriptionCollection
    {
        public Dictionary<string, BuildingDescription> Descriptions { get; }

        public BuildingsDescriptionCollection(Dictionary<string, object> descriptions, DescriptionFactory factory)
        {
            Descriptions = new Dictionary<string, BuildingDescription>();

            foreach (var description in descriptions)
            {
                Descriptions.Add(description.Key,
                    factory.Create<BuildingDescription>(description.Key ,(Dictionary<string, object>)descriptions[description.Key]));
            }
        }
    }
}