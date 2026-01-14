using UnityEngine.UIElements;

namespace Runtime.UI.InGameMenu.LoadMenu
{
    public class LoadMenuView
    {
        public VisualElement Root { get; }
        public VisualElement SavesList { get; }
        public Button RefreshButton { get; }

        public LoadMenuView(VisualTreeAsset asset)
        {
            Root = asset.CloneTree().Q<VisualElement>("load-content");
            
            SavesList = Root.Q<VisualElement>("container");
            RefreshButton = Root.Q<Button>("refresh-button");
        }
    }
}