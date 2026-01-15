using Runtime.ViewDescriptions.Stats;
using UnityEngine.UIElements;

namespace Runtime.Colony.Buildings.Selection.BuildingPanel
{
    public class SelectedPanelView
    {
        public VisualElement Root { get; }
        
        public StatViewDescriptionCollection StatViewDescriptions { get; }
        
        public SelectedPanelView(VisualTreeAsset buildingPanelAsset, StatViewDescriptionCollection statViewDescriptions)
        {
            Root = buildingPanelAsset.CloneTree().Q<VisualElement>("selection-panel");
            
            StatViewDescriptions = statViewDescriptions;
        }
    }
}