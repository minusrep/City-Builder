using System.Collections.Generic;

namespace Runtime.Colony.Orders
{
    public class ColonyOrdersPoolModel
    {
        public Dictionary<string, ColonyOrder> AvailableOrders { get; private set; }
        public Dictionary<string, ColonyOrder> ClaimedOrders { get; private set; }
        public ColonyOrderDescriptionCollection ColonyOrderDescriptionCollections;

        public void AddOrder(ColonyOrder order)
        {
            /*AvailableOrders.Add();*/
        }
    }
}