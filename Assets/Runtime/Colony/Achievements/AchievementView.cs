using UnityEngine.UIElements;

namespace Runtime.Colony.Achievements
{
    public class AchievementView
    {
        public VisualElement Root { get; }
        public VisualElement Icon { get; }
        public Label Title { get; }
        public Label Description { get; }

        public AchievementView(VisualTreeAsset asset)
        {
            Root = asset.CloneTree().Q<VisualElement>("achievement");
            
            Icon = Root.Q<VisualElement>("icon");
            Title = Root.Q<Label>("title");
            Description = Root.Q<Label>("description");
        }
    }
}