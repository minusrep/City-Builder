using System.Collections.Generic;
using System.Linq;

namespace Runtime.Colony.Orders
{
    public class OrderManager : IOrderManager
    {
        public Dictionary<int, OrderModel> AvailableOrders { get; } = new();
        private Dictionary<int, OrderModel> ClaimedOrders { get; } = new();
        
        private int _index;

        public int AddOrder(OrderModel orderModel)
        {
            AvailableOrders.Add(_index, orderModel);
            
            _index++;
            
            return orderModel.Id;
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

        public bool TryDequeueAnyAvailable(out OrderModel orderModel)
        {
            orderModel = null;
            if (AvailableOrders.Count != 0)
            {
                var firstKey = AvailableOrders.Keys.First();
                orderModel = AvailableOrders[firstKey];
                AvailableOrders.Remove(firstKey);
                ClaimedOrders[firstKey] = orderModel;
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

        public List<OrderModel> GetAvailableOrdersSnapshot()
        {
            return AvailableOrders.Values.ToList();
        }

        public bool TryGetOrder(int orderId, out OrderModel orderModel)
        {
            if (!ClaimedOrders.TryGetValue(orderId, out orderModel))
            {
                if (!AvailableOrders.TryGetValue(orderId, out orderModel))
                {
                    return false;
                }
            }

            return true;
        }

        public void RemoveOrder(OrderModel orderModel)
        {
            
        }
    }
}