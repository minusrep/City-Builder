using System.Collections.Generic;

namespace Runtime.Descriptions.Items
{
    public class ItemDescriptionCollection
    {
        public Dictionary<string, ResourceDescription> Descriptions { get; }

        public ItemDescriptionCollection(Dictionary<string, object> descriptions)
        {
            Descriptions = new Dictionary<string, ResourceDescription>();

            foreach (var description in descriptions)
            {
                var dict = (Dictionary<string, object>)description.Value;
                Descriptions.Add(description.Key, new ResourceDescription(dict));
            }
        }
    }
}