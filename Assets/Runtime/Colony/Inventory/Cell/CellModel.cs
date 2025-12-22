using System;
using System.Collections.Generic;
using Runtime.Descriptions.Items;
using Runtime.ModelCollections;

namespace Runtime.Colony.Inventory.Cell
{
    public class CellModel : ISerializeModel
    {
        public event Action OnChanged;
        public ResourceDescription Resource { get; private set; }
        public int Amount { get; private set; }

        public bool TryAdd(ResourceDescription resource, int amount, int maxAmount)
        {
            if (Amount == 0)
            {
                if (amount > maxAmount)
                {
                    return false;
                }

                Resource = resource;
            }

            if (Amount + amount > maxAmount)
            {
                return false;
            }

            Amount += amount;
            
            OnChanged?.Invoke();
            
            return true;
        }

        public bool TryReduce(int amount)
        {
            if (Amount < amount)
            {
                return false;
            }

            Amount -= amount;

            if (Amount == 0)
            {
                Clear();
            }
            
            OnChanged?.Invoke();

            return true;
        }

        private void Clear()
        {
            Resource = null;
            Amount = 0;
        }

        public Dictionary<string, object> Serialize() => new()
        {
            { "amount", Amount },
            { "resource", Resource == null ? "wood" : Resource.Id },
        };
    }
}