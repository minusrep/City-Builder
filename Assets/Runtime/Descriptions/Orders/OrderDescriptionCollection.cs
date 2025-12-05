using System.Collections.Generic;

namespace Runtime.Descriptions.Orders
{
    public class OrderDescriptionCollection
    {
        public Dictionary<string, OrderDescription> OrderDescriptions;

        public OrderDescriptionCollection(Dictionary<string, OrderDescription> descriptions)
        {
            OrderDescriptions = descriptions;
        }
    }
}