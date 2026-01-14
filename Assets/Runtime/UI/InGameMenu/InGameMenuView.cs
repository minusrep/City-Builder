using UnityEngine.UIElements;

namespace Runtime.UI.InGameMenu
{
    public class InGameMenuView
    {
        public VisualElement Root { get; }
        public VisualElement PageContent { get; }
        public Button ResumeButton { get; }
        public Button SaveButton { get; }
        public Button LoadButton { get; }
        public Button AchievementsButton { get; }
        public Button ExitButton { get; }

        public InGameMenuView(VisualTreeAsset inGameMenuAsset)
        {
            Root = inGameMenuAsset.CloneTree().Q<VisualElement>(InGameMenuConstants.Content);
            
            PageContent = Root.Q<VisualElement>(InGameMenuConstants.PageContent);
            
            ResumeButton = Root.Q<Button>(InGameMenuConstants.ResumeButton);
            SaveButton = Root.Q<Button>(InGameMenuConstants.SaveButton);
            LoadButton = Root.Q<Button>(InGameMenuConstants.LoadButton);
            AchievementsButton = Root.Q<Button>(InGameMenuConstants.AchievementsButton);
            ExitButton = Root.Q<Button>(InGameMenuConstants.ExitButton);
        }
    }
}