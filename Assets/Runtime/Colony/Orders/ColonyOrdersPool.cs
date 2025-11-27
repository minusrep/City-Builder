using System.Collections.Generic;

namespace Runtime.Colony.Orders
{
    public class ColonyOrdersPool
    {
        public Dictionary<string, ColonyOrder> AvailableOrders { get; private set; }
        public Dictionary<string, ColonyOrder> ClaimedOrders { get; private set; }
        public ColonyOrderDescriptionCollection ColonyOrderDescriptionCollections { get; private set; }

        public void AddOrder(ColonyOrder order)
        {

        }
    }
}