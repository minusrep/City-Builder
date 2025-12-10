using Runtime.Systems;
using UnityEngine.UIElements;

namespace Runtime.Colony.Inventory
{
    public class InventoryView
    {
        public VisualTreeAsset CellAsset { get; }
        public VisualElement MenuRoot { get; }
        
        public InventoryView(MenuContent menuRoot, VisualTreeAsset cellAsset)
        {
            MenuRoot = menuRoot.MenuRoot;
            CellAsset = cellAsset;
        }
    }
}