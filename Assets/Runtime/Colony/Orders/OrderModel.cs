using System;
using System.Collections.Generic;
using Runtime.Extensions;
using Runtime.ModelCollections;

namespace Runtime.Colony.Orders
{
    public class OrderModel : ISerializeModel, IDeserializeModel
    {
        public event Action<OrderModel> OnAmountChanged;
        
        public string Id { get; set; }
        public string FromBuildingId { get; private set; }

        public string Type { get; set; }
        public string ResourceId { get; set; }

        private int _amount;
        public int Amount 
        { 
            get => _amount;
            set
            {
                _amount = value;
                OnAmountChanged?.Invoke(this);
            }
        }

        public OrderModel(string id, string fromBuildingId)
        {
            Id = id;
            FromBuildingId = fromBuildingId;
        }

        public OrderModel() : this("", "") { }

        public Dictionary<string, object> Serialize() => new()
        {
            { "id", Id },
            { "from_building_id", FromBuildingId },
            { "type", Type },
            { "resource", ResourceId },
            { "amount", Amount }
        };

        public void Deserialize(Dictionary<string, object> data)
        {
            Id = data.GetString("id");
            FromBuildingId =  data.GetString("from_building_id");
            Type = data.GetString("type");
            ResourceId = data.GetString("resource");
            Amount = data.GetInt("amount");
        }
    }
}