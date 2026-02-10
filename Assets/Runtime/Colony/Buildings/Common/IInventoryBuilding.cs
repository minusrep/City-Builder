using Runtime.Colony.Inventory;
using Runtime.Descriptions.Items;

namespace Runtime.Colony.Buildings.Common
{
    public interface IInventoryBuilding
    {
        InventoryModel Inventory { get; }

        public bool TryAddItem(ResourceDescription resourceDescription, int amount);

        public bool TryRemoveItem(ResourceDescription resourceDescription, int amount);
    }
}