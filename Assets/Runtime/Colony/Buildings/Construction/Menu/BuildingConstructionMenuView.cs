using UnityEngine.UIElements;

namespace Runtime.Colony.Buildings.Construction.Menu
{
    public class BuildingConstructionMenuView
    {
        public VisualElement Root { get; }
        public VisualElement ConstructionPanel { get; }
        public VisualElement BuildingList { get; }
        public Button ToggleButton { get; }

        public BuildingConstructionMenuView(VisualTreeAsset constructionMenuAsset)
        {
            Root = constructionMenuAsset.CloneTree().Q<VisualElement>("construction-content");
            ConstructionPanel = Root.Q<VisualElement>("construction-panel");
            BuildingList = Root.Q<VisualElement>("building-list");
            ToggleButton = Root.Q<Button>("construction-toggle-button");
        }
    }
}