using System;
using System.Collections.Generic;
using Runtime.Colony.Inventory;
using Runtime.Descriptions.Items;
using Runtime.Extensions;
using Runtime.ModelCollections;

namespace Runtime.Colony.Items
{
    public class ItemModel : ISerializeModel, IDeserializeModel, IInventoryItem
    {
        public ItemDescription Description { get; }
        public string Type => Description.Type;
        public int Amount { get; set; }
        public int MaxAmount { get; set; }

        public ItemModel(ItemDescription description)
        {
            Description = description;
        }

        public void IncreaseAmount(int amount)
        {
            if (amount > 0)
            {
                Amount += amount;
            }
        }

        public void ReduceAmount(int amount)
        {
            if (amount > 0)
            {
                var taken = Math.Min(Amount, amount);
                Amount -= taken;
            }
        }

        public Dictionary<string, object> Serialize()
        {
            return new Dictionary<string, object>
            {
                { "amount", Amount },
                { "description", Description.Type }
            };
        }

        public void Deserialize(Dictionary<string, object> data)
        {
            Amount = data.GetInt("amount");
        }
    }
}