using UnityEngine.UIElements;

namespace Runtime.UI.InGameMenu.LoadMenu
{
    public class LoadMenuView
    {
        public VisualElement Root { get; }

        public LoadMenuView(VisualTreeAsset asset)
        {
            Root = asset.CloneTree().Q<VisualElement>("content");
        }
    }
}