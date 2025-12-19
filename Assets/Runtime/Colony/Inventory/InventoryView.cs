using UnityEngine.UIElements;

namespace Runtime.Colony.Inventory
{
    public class InventoryView
    {
        public VisualTreeAsset CellAsset { get; }
        public VisualElement Root { get; private set; }

        public InventoryView(UIDocument document, VisualTreeAsset cellAsset)
        {
            CellAsset = cellAsset;
            Root = document.rootVisualElement.Q<VisualElement>("content");
        }
    }
}