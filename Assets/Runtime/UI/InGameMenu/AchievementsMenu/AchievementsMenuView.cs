using Runtime.Colony.Achievements;
using Runtime.ViewDescriptions;
using UnityEngine.UIElements;

namespace Runtime.UI.InGameMenu.AchievementsMenu
{
    public class AchievementsMenuView
    {
        public VisualElement Root { get; }
        public VisualElement Container { get; }

        public AchievementsMenuView(VisualTreeAsset asset)
        {
            Root = asset.CloneTree().Q<VisualElement>("achievements-content");
            Container = Root.Q<VisualElement>("achievements-container");
        }

        public AchievementView DrawAchievement(WorldViewDescriptions worldViewDescriptions)
        {
            var achievementAsset = worldViewDescriptions.AchievementsViewDescription.AchievementAsset;
            var achievementView = new AchievementView(achievementAsset);

            return achievementView;
        }
    }
}