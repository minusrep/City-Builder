using System;
using System.Collections.Generic;
using Runtime.Descriptions.Items;
using Runtime.Extensions;
using Runtime.ModelCollections;

namespace Runtime.Colony.Orders
{
    public class OrderModel : ISerializeModel, IDeserializeModel
    {
        public Action<OrderModel> OnAmountChanged;
        
        public string Id { get; set; }
        public string FromBuildingId { get; }

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
        
        private int _selectedAmount;

        public int FreeAmount => Amount - _selectedAmount;

        public OrderModel(string id, string fromBuildingId)
        {
            Id = id;
            FromBuildingId = fromBuildingId;
        }

        public Dictionary<string, object> Serialize() => new()
        {
            { "id", Id },
            { "type", Type },
            { "resource", ResourceId },
            { "amount", Amount },
            { "selected_amount", _selectedAmount }
        };

        public void Deserialize(Dictionary<string, object> data)
        {
            Id = data.GetString("id");
            Type = data.GetString("type");
            ResourceId = data.GetString("resource");
            Amount = data.GetInt("amount");
            _selectedAmount = data.GetInt("selected_amount");
        }

        public void Select(int amount)
        {
            _selectedAmount += Math.Min(amount, FreeAmount) ;
        }

        public void Done(int amount)
        {
            amount = Math.Min(amount, _selectedAmount);
            _selectedAmount -= amount;
            Amount -= amount;
        }
    }
}