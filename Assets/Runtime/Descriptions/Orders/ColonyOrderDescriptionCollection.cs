using System.Collections.Generic;

namespace Runtime.Descriptions.Orders
{
    public class ColonyOrderDescriptionCollection
    {
        public Dictionary<string, ColonyOrderDescription> OrderDescriptions;

        public ColonyOrderDescriptionCollection(Dictionary<string, ColonyOrderDescription> descriptions)
        {
            OrderDescriptions = descriptions;
        }
    }
}