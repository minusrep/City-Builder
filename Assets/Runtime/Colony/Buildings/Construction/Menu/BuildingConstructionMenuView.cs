using UnityEngine.UIElements;

namespace Runtime.Colony.Buildings.Construction.Menu
{
    public class BuildingConstructionMenuView
    {
        public VisualElement Root { get; }
        public VisualElement BuildingList { get; }

        public BuildingConstructionMenuView(VisualTreeAsset constructionMenuAsset)
        {
            Root = constructionMenuAsset.CloneTree().Q<VisualElement>("construction-content");
            BuildingList = Root.Q<VisualElement>("building-list");
        }
    }
}