using UnityEngine.UIElements;

namespace Runtime.Colony.Buildings.Selection.BuildingPanel
{
    public class BuildingPanelView
    {
        public VisualElement Root { get; }

        public BuildingPanelView(VisualTreeAsset buildingPanelAsset)
        {
            Root = buildingPanelAsset.CloneTree().Q<VisualElement>("building-panel");
        }
    }
}