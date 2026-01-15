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
        
        public SelectedPanelView(VisualTreeAsset buildingPanelAsset, StatViewDescriptionCollection statViewDescriptions, InventoryViewDescription inventoryViewDescription)
        {
            Root = buildingPanelAsset.CloneTree().Q<VisualElement>("selection-panel");
            
            StatViewDescriptions = statViewDescriptions;
            InventoryViewDescription = inventoryViewDescription;
        }
    }
}