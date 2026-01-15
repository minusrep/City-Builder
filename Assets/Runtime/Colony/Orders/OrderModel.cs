using System;
using System.Collections.Generic;
using Runtime.Extensions;
using Runtime.ModelCollections;

namespace Runtime.Colony.Orders
{
    public class OrderModel : ISerializeModel, IDeserializeModel
    {
        public event Action<OrderModel> OnAmountChanged;
        public event Action<OrderModel> OnReservedAmountChanged;
        
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

        private int _reservedAmount;
        
        public int AvailableAmount => Amount - _reservedAmount;
        
        public OrderModel(string id, string fromBuildingId)
        {
            Id = id;
            FromBuildingId = fromBuildingId;
        }

        public OrderModel() : this("", "") { }

        public void Reserve(int amount)
        {
            _reservedAmount += Math.Min(AvailableAmount, amount);
            OnReservedAmountChanged?.Invoke(this);
        }

        public void Unreserve(int amount)
        {
            _reservedAmount -= Math.Min(_reservedAmount, amount);
            OnReservedAmountChanged?.Invoke(this);
        }

        public void Complete(int amount)
        {
            amount = Math.Min(_reservedAmount, amount);
            _reservedAmount -= amount;
            Amount -= amount;
        }
        
        public Dictionary<string, object> Serialize() => new()
        {
            { "id", Id },
            { "from_building_id", FromBuildingId },
            { "type", Type },
            { "resource", ResourceId },
            { "amount", Amount },
            { "reserved_amount", _reservedAmount }
        };

        public void Deserialize(Dictionary<string, object> data)
        {
            Id = data.GetString("id");
            FromBuildingId =  data.GetString("from_building_id");
            Type = data.GetString("type");
            ResourceId = data.GetString("resource");
            Amount = data.GetInt("amount");
            _reservedAmount = data.GetInt("reserved_amount");
        }
    }
}