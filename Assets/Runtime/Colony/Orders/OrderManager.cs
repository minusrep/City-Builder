using System.Collections.Generic;
using System.Linq;
using Runtime.ModelCollections;

namespace Runtime.Colony.Orders
{
    public class OrderManager: ISerializeModel, IDeserializeModel 
    {
        private Queue<OrderModel> _orders = new();

        public void AddOrder(OrderModel order)
        {
            var existingOrder = _orders.FirstOrDefault(o => o.Id == order.Id);

            if (existingOrder != null)
            {
                existingOrder.Amount += order.Amount;
                return;
            }
            
            _orders.Enqueue(order);
            order.OnAmountChanged += OnAmountChanged;
        }

        public OrderModel TakeOrder()
        {
            return _orders.Peek();
        }

        public bool HasOrders()
        {
            return _orders.Count > 0;
        }

        public Dictionary<string, object> Serialize()
        {
            var orders = _orders.Select(order => order.Serialize()).Cast<object>().ToList();

            return new Dictionary<string, object>()
            {
                { "orders", orders }
            };
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            var ordersRaw = (List<object>)data["orders"];

            _orders = new Queue<OrderModel>();
            foreach (var orderRaw in ordersRaw)
            {
                var order = new OrderModel();
                order.Deserialize((Dictionary<string, object>)orderRaw);
                AddOrder(order);
            }
        }
        
        private void OnAmountChanged(OrderModel order)
        {
            if (order.Id == _orders.Peek().Id && order.Amount <= 0)
            {
                order.OnAmountChanged -= OnAmountChanged;
                _orders.Dequeue();
            }
        }
    }
}