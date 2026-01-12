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
            var currentOrder = _orders.FirstOrDefault(o => o.Id == order.Id && o.FromBuildingId == order.FromBuildingId);

            if (currentOrder != null)
            {
                currentOrder.Amount = order.Amount;
            }
            else
            {
                _orders.Enqueue(order);
                order.OnSelected += OnOrderSelected;    
            }
        }
        
        public OrderModel TakeOrder()
        {
            return _orders.Peek();
        }

        public void RestoreOrder(OrderModel order)
        {
            var currentOrder = _orders.FirstOrDefault(o => o.Id == order.Id && o.FromBuildingId == order.FromBuildingId);

            if (currentOrder != null)
            {
                currentOrder.Deselect(order.Amount);
            }
            else
            {
                AddOrder(order);
            }
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
                var order = new OrderModel("", "");
                order.Deserialize((Dictionary<string, object>)orderRaw);
                AddOrder(order);
            }
        }

        private void OnOrderSelected(OrderModel order)
        {
            if (order == _orders.Peek() && order.FreeAmount <= 0)
            {
                order.OnSelected -= OnOrderSelected;
                _orders.Dequeue();
            }
        }
    }
}