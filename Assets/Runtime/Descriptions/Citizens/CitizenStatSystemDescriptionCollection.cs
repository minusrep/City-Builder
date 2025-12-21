using System.Collections.Generic;

namespace Runtime.Descriptions.Citizens
{
    public class CitizenStatSystemDescriptionCollection
    {
        public Dictionary<string, CitizenStatSystemDescription> Descriptions { get; private set; }

        public CitizenStatSystemDescriptionCollection(Dictionary<string, object> data)
        {
            Descriptions = new Dictionary<string, CitizenStatSystemDescription>();
            
            foreach (var system in data)
            {
                Descriptions.Add(system.Key, new CitizenStatSystemDescription(system.Value as Dictionary<string, object>));
            }
        }

        public CitizenStatSystemDescription Get(string name)
        {
            return Descriptions[name];
        }
    }
}