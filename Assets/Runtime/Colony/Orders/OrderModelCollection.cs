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

        public override void Add(string key, OrderModel model)
        {
            if (Models.TryGetValue(key, out var order))
            {
                order.Amount += model.Amount;
                return;
            }
            
            base.Add(key, model);
            model.OnAmountChanged += OnAmountChanged;
        }

        protected override OrderModel CreateModelFromData(string id, Dictionary<string, object> data)
        {
            var order = new OrderModel(id, _buildingId);
            order.Deserialize(data);

            return order;
        }

        protected override OrderModel CreateModel()
        {
            var order = new OrderModel(Index.ToString(), _buildingId);
            return order;
        }
        
        private void OnAmountChanged(OrderModel model)
        {
            if (model.Amount <= 0)
            {
                model.OnAmountChanged -= OnAmountChanged;
                Remove(model.Id);
            }
        }
    }
}