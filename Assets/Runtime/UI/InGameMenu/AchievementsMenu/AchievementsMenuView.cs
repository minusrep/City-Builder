using UnityEngine.UIElements;

namespace Runtime.UI.InGameMenu.AchievementsMenu
{
    public class AchievementsMenuView
    {
        public VisualElement Root { get; }

        public AchievementsMenuView(VisualTreeAsset asset)
        {
            Root = asset.CloneTree().Q<VisualElement>("achievements-content");
        }
    }
}