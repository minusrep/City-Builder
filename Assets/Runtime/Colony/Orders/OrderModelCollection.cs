using System;
using System.Collections.Generic;
using Runtime.Colony.ModelCollections;
using Runtime.Colony.Orders.Types;
using Runtime.Descriptions.Orders;

namespace Runtime.Colony.Orders
{
    public class OrderModelCollection : ISerializeModel, IDeserializeModel
    {
        public event Action<OrderModel> OnCreateOrder;
        public event Action<OrderModel> OnClaimedOrder;
        public event Action<OrderModel> OnDeleteOrder;
        public Dictionary<int, OrderModel> AvailableOrders { get; private set; } = new();
        public Dictionary<int, OrderModel> ClaimedOrders { get; private set; } = new();

        private OrderDescriptionCollection _descriptions;
        private int _index;

        public OrderModelCollection(OrderDescriptionCollection descriptions)
        {
            _descriptions = descriptions;
        }

        public void Create(OrderModel order)
        {
            var description = _descriptions.OrderDescriptions[order.Type];

            order.Id = _index++;
            order.Description = description;

            AvailableOrders.Add(order.Id, order);

            OnCreateOrder?.Invoke(order);
        }

        public void Claim(int orderId, int toId, List<int> citizensId)
        {
            var order = AvailableOrders[orderId];
            AvailableOrders.Remove(orderId);

            order.ToBuildingId = toId;
            order.CitizensId = citizensId;

            ClaimedOrders.Add(order.Id, order);

            OnClaimedOrder?.Invoke(order);
        }

        public void Delete(int id)
        {
            var order = AvailableOrders[id];

            AvailableOrders.Remove(id);

            OnDeleteOrder?.Invoke(order);
        }

        public Dictionary<string, object> Serialize()
        {
            var data = new Dictionary<string, object> { { "index", _index } };

            var availableOrders = new Dictionary<string, object>();
            var claimedOrders = new Dictionary<string, object>();

            foreach (var order in AvailableOrders)
            {
                ExtractData(order, availableOrders);
            }

            foreach (var order in ClaimedOrders)
            {
                ExtractData(order, claimedOrders);
            }

            data.Add("available_orders", availableOrders);
            data.Add("claimed_orders", claimedOrders);

            return data;

            void ExtractData(KeyValuePair<int, OrderModel> order, Dictionary<string, object> orders)
            {
                var orderData = order.Value.Serialize();
                orders.Add(order.Key.ToString(), orderData);
            }
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            var availableOrders = new Dictionary<int, OrderModel>();
            var claimedOrders = new Dictionary<int, OrderModel>();

            foreach (var order in (Dictionary<string, object>)data["available_orders"])
            {
                FillData(order, availableOrders);
            }

            foreach (var order in (Dictionary<string, object>)data["claimed_orders"])
            {
                FillData(order, claimedOrders);
            }

            _index = Convert.ToInt32(data["index"]);

            AvailableOrders = availableOrders;
            ClaimedOrders = claimedOrders;

            return;

            void FillData(KeyValuePair<string, object> order, Dictionary<int, OrderModel> orders)
            {
                var orderData = (Dictionary<string, object>)order.Value;

                var orderId = Convert.ToInt32(order.Key);

                var orderInstance = CreateModelFromData(orderData, orderId);

                orders.Add(Convert.ToInt32(orderId), orderInstance);
            }
        }

        private OrderModel CreateModelFromData(Dictionary<string, object> data, int orderId)
        {
            var orderType = data["type"].ToString();
            var description = _descriptions.OrderDescriptions[orderType];

            switch (orderType)
            {
                case OrderConstants.OrderTypes.Delivery:
                {
                    var fromBuildingId = Convert.ToInt32(data["from_building_id"]);
                    var order = new DeliveryOrderModel(fromBuildingId);
                    order.Deserialize(data);
                    order.Description = description;
                    order.Id = orderId;

                    return order;
                }
            }

            return null;
        }
    }
}