using UnityEngine.UIElements;

namespace Runtime.ViewDescriptions
{
    public class LoadPanel
    {
        public VisualElement Root { get; }
        public Image Image { get; }
        public Label Title { get; }
        public Label Date { get; }
        public Label Time { get; }
        public Label Size { get; }
        public Button DeleteButton { get; }
        public Button LoadButton { get; }

        public LoadPanel(VisualTreeAsset asset)
        {
            Root = asset.CloneTree().Q<VisualElement>("load-panel");

            Image = Root.Q<Image>("image");
            Title = Root.Q<Label>("title-text");
            Date = Root.Q<Label>("date-text");
            Time = Root.Q<Label>("time-text");

            LoadButton = Root.Q<Button>("load-button");
            DeleteButton = Root.Q<Button>("delete-button");
        }
    }
}