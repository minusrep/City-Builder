namespace Runtime.Colony.Inventory
{
    public class CellModel
    {
        public IInventoryItem Item { get; private set; }
        public int Amount { get; private set; }
        
        public bool TryAdd(IInventoryItem item, int amount)
        {
            if (Item == null)
            {
                Item = item;
                Amount = amount;
                return true;
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

        public void Clear()
        {
            Item = null;
            Amount = 0;
        }
    }
}