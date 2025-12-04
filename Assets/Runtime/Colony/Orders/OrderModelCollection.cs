using System;
using System.Collections;
using System.Collections.Generic;
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

        private ColonyOrderDescriptionCollection _description;
        private int _index;

        public OrderModelCollection(ColonyOrderDescriptionCollection description)
        {
            _description = description;
        }

        public void CreateDeliveryOrder(int fromId)
        {
            var description = _description.OrderDescriptions["delivery"];

            var order = new DeliveryOrderModel(description, fromId)
            {
                Id = _index++
            };

            AvailableOrders.Add(order.Id, order);

            OnCreateOrder?.Invoke(order);
        }

        public void ClaimOrder(int orderId, int toId, List<int> citizensId)
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

                int orderId = Convert.ToInt32(order.Key);
                
                var orderInstance = CreateModelFromData(orderData, orderId);
                
                orders.Add(Convert.ToInt32(orderId), orderInstance);
            }
        }

        private OrderModel CreateModelFromData(Dictionary<string, object> data, int orderId)
        {
            var description = _description.OrderDescriptions["delivery"];

            var fromBuildingId = Convert.ToInt32(data["from_building_id"]);
            var toBuildingId = Convert.ToInt32(data["to_building_id"]);

            var raw = data["citizens_id"] as IEnumerable ?? Array.Empty<object>();

            var citizensIds = new List<int>();

            foreach (var id in raw)
            {
                citizensIds.Add(Convert.ToInt32(id));
            }
            
            var order = new DeliveryOrderModel(description, fromBuildingId)
            {
                ToBuildingId = toBuildingId,
                CitizensId = citizensIds,
                Id = orderId
            };

            return order;
        }
    }
}