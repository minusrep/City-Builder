using Runtime.Descriptions.GameResources;
using Runtime.Colony.ModelCollections;
using System.Collections.Generic;
using System;
using Runtime.Utilities;

namespace Runtime.Colony.GameResources
{
    public class ResourceModel : ISerializeModel, IDeserializeModel
    {
        private ResourceDescription Description { get; }

        public int Amount { get; private set; }

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