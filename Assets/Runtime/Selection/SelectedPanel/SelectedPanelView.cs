using Runtime.ViewDescriptions.Inventory;
using Runtime.ViewDescriptions.Stats;
using UnityEngine.UIElements;

namespace Runtime.Selection.SelectedPanel
{
    public class SelectedPanelView
    {
        public VisualElement Root { get; }
        
        public StatViewDescriptionCollection StatViewDescriptions { get; }
        
        public InventoryViewDescription InventoryViewDescription { get; }
        
        public SelectedPanelView(VisualElement root, StatViewDescriptionCollection statViewDescriptions, InventoryViewDescription inventoryViewDescription)
        {
            Root = root;
            
            StatViewDescriptions = statViewDescriptions;
            InventoryViewDescription = inventoryViewDescription;
        }
    }
}