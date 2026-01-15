using Runtime.ViewDescriptions.Inventory;
using Runtime.ViewDescriptions.Items;
using UnityEngine.UIElements;

namespace Runtime.Colony.Inventory.Cell
{
    public class CellView
    {
        public VisualElement Root { get; }
        public VisualElement Image { get; }
        public Label Amount { get; }
        
        public ItemViewDescriptionCollection Description { get; }

        public CellView(VisualTreeAsset root, ItemViewDescriptionCollection description)
        {
            Description = description;
            Root = root.CloneTree().Q<VisualElement>("cell");

            Image = Root.Q<VisualElement>("image");
            Amount = Root.Q<Label>("amount");
        }
    }
}