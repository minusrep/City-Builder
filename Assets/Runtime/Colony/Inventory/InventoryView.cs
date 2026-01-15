using Runtime.ViewDescriptions.Inventory;
using UnityEngine.UIElements;

namespace Runtime.Colony.Inventory
{
    public class InventoryView
    {
        public VisualTreeAsset CellAsset => ViewDescription.CellViewAsset;
        public VisualElement Root { get; private set; }

        public InventoryViewDescription ViewDescription { get; private set; }

        public InventoryView(VisualElement root, InventoryViewDescription viewDescription)
        {
            ViewDescription =  viewDescription;
            
            Root = root.Q<VisualElement>("content");
        }
    }
}