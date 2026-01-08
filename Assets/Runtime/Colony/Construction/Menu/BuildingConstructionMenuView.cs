using UnityEngine.UIElements;

namespace Runtime.Colony.Construction.Menu
{
    public class BuildingConstructionMenuView
    {
        public VisualElement Root { get; }
        public VisualElement BuildingList { get; }

        public BuildingConstructionMenuView(VisualTreeAsset constructionMenuAsset)
        {
            Root = constructionMenuAsset.CloneTree().Q<VisualElement>("content");
            BuildingList = Root.Q<VisualElement>("building-list");
        }
    }
}