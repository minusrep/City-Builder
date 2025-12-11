using Runtime.Descriptions.GameResources;
using System.Collections.Generic;
using System;
using Runtime.Colony.Inventory;
using Runtime.Utilities;

namespace Runtime.Colony.GameResources
{
    public class ResourceModel : ISerializeModel, IDeserializeModel, IInventoryItem
    {
        public ResourceDescription Description { get; }
        public string Type => Description.Type;
        public int Amount { get; set; }
        public int MaxAmount { get; set; }

        public ResourceModel(ResourceDescription description)
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