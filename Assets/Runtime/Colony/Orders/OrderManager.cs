using System.Collections.Generic;
using System.Linq;
using Runtime.ModelCollections;

namespace Runtime.Colony.Orders
{
    public class OrderManager: ISerializeModel, IDeserializeModel 
    {
        private List<OrderModel> _orders = new();

        public OrderModel this[string id] => _orders.First(o => o.Id == id);

        public void AddOrder(OrderModel order)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.Id == order.Id);

            if (existingOrder != null)
            {
                existingOrder.Amount += order.Amount;
                return;
            }
            
            _orders.Add(order);
            order.OnAmountChanged += OnAmountChanged;
            order.OnReservedAmountChanged += OnReservedAmountChanged;
        }

        public OrderModel TakeOrder()
        {
            return _orders.First(o => o.AvailableAmount > 0);
        }

        public bool HasOrders()
        {
            return _orders.Any(o => o.AvailableAmount > 0);
        }

        public bool Contains(string id)
        {
            return _orders.Any(o => o.Id == id);
        }

        public void ToBack(string id)
        {
            var order = this[id];
            if (order != null)
            {
                _orders.Remove(order);
                _orders.Add(order);
            }
        }
        
        public Dictionary<string, object> Serialize()
        {
            var orders = _orders.Select(order => order.Serialize()).Cast<object>().ToList();

            return new Dictionary<string, object>
            {
                { "orders", orders }
            };
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            var ordersRaw = (List<object>)data["orders"];

            _orders = new List<OrderModel>();
            foreach (var orderRaw in ordersRaw)
            {
                var order = new OrderModel();
                order.Deserialize((Dictionary<string, object>)orderRaw);
                AddOrder(order);
            }
        }
        
        private void OnAmountChanged(OrderModel order)
        {
            if (order.Amount <= 0)
            {
                
                order.OnReservedAmountChanged -= OnReservedAmountChanged;
                order.OnAmountChanged -= OnAmountChanged;
                _orders.Remove(order);
            }
        }
        
        private void OnReservedAmountChanged(OrderModel order)
        {
            if (order.AvailableAmount <= 0)
            {
                _orders.Remove(order);
                _orders.Add(order);
            }
        }
    }
}