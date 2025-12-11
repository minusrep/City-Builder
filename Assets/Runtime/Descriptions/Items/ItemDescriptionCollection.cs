using System.Collections.Generic;

namespace Runtime.Descriptions.Items
{
    public class ItemDescriptionCollection
    {
        public Dictionary<string, ItemDescription> Descriptions { get; }

        public ItemDescriptionCollection(Dictionary<string, object> descriptions)
        {
            Descriptions = new Dictionary<string, ItemDescription>();

            foreach (var description in descriptions)
            {
                var dict = (Dictionary<string, object>)description.Value;
                Descriptions.Add(description.Key, new ItemDescription(dict));
            }
        }
    }
}