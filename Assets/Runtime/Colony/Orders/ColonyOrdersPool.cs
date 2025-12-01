using System.Collections.Generic;
using System.Linq;

namespace Runtime.Colony.Orders
{
    public class ColonyOrdersPool
    {
        public int AvailableCount => AvailableOrders.Count;
        
        public int ClaimedCount => ClaimedOrders.Count;

        public Dictionary<int, ColonyOrder> AvailableOrders { get; } = new();
        private Dictionary<int, ColonyOrder> ClaimedOrders { get; } = new();
        
        private int _nextOrderId = 1;

        public int AddOrder(ColonyOrder order)
        {
            if (order.Id == 0)
            {
                order.Id = GetNextOrderId();
            }

            var key = order.Id;
            AvailableOrders[key] = order;
            return order.Id;

        }

        public bool TryClaimOrder(int orderId, int citizenId)
        {
            if (AvailableOrders.Remove(orderId, out var order))
            {
                order.CitizensId.Add(citizenId);
                ClaimedOrders[orderId] = order;
                return true;
            }

            return false;
        }

        public bool TryDequeueAnyAvailable(out ColonyOrder order)
        {
            order = null;
            if (AvailableOrders.Count != 0)
            {
                var firstKey = AvailableOrders.Keys.First();
                order = AvailableOrders[firstKey];
                AvailableOrders.Remove(firstKey);
                ClaimedOrders[firstKey] = order;
                return true;
            }

            return false;
        }

        public bool CompleteOrder(int orderId)
        {
            return ClaimedOrders.Remove(orderId);
        }
        
        public bool CancelOrder(int orderId)
        {
            return AvailableOrders.Remove(orderId) || ClaimedOrders.Remove(orderId);
        }

        public List<ColonyOrder> GetAvailableOrdersSnapshot()
        {
            return AvailableOrders.Values.ToList();
        }

        public bool TryGetOrder(int orderId, out ColonyOrder order)
        {
            if (!ClaimedOrders.TryGetValue(orderId, out order))
            {
                if (!AvailableOrders.TryGetValue(orderId, out order))
                {
                    return false;
                }
            }

            return true;
        }
        
        private int GetNextOrderId()
        {
            return _nextOrderId++;
        }
    }
}