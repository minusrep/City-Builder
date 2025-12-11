using System.Collections.Generic;
using Runtime.ModelCollections;

namespace Runtime.Colony.Inventory.Cell
{
    public class CellModel : ISerializeModel
    {
        public IInventoryItem Item { get; private set; }
        public int Amount { get; private set; }

        public bool TryAdd(IInventoryItem item, int amount)
        {
            if (Amount == 0)
            {
                if (amount > item.MaxAmount)
                {
                    return false;
                }

                Item = item;
            }

            if (Amount + amount > Item.MaxAmount)
            {
                return false;
            }

            Amount += amount;
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

            return true;
        }

        private void Clear()
        {
            Item = null;
            Amount = 0;
        }

        public Dictionary<string, object> Serialize() => new()
        {
            { "item", Item }, //TODO: Убедится, сохранять ли сам Item
            { "amount", Amount }
        };
    }
}