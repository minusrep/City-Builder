using Runtime.Systems;
using UnityEngine.UIElements;

namespace Runtime.Colony.Inventory
{
    public class InventoryView
    {
        public VisualTreeAsset CellAsset { get; }
        public VisualElement WorldRoot { get; }
        
        public InventoryView(MenuContent content, VisualTreeAsset cellAsset)
        {
            WorldRoot = content.WorldRoot;
            CellAsset = cellAsset;
        }
    }
}