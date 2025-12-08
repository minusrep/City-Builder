using System.Collections.Generic;
using Runtime.ModelCollections;

namespace Runtime.Colony.Orders
{
    public class OrderModelCollection : UniformModelCollection<OrderModel>
    {
        private readonly string _buildingId;

        public OrderModelCollection(string buildingId) : base(string.Empty)
        {
            _buildingId = buildingId;
        }

        protected override OrderModel CreateModelFromData(string id, Dictionary<string, object> data)
        {
            var order = new OrderModel(GetCurrentId(id), _buildingId);
            order.Deserialize(data);

            return order;
        }

        protected override OrderModel CreateModel()
        {
            var order = new OrderModel(Index, _buildingId);
            return order;
        }
    }
}