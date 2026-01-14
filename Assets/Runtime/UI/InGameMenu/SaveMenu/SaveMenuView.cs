using UnityEngine.UIElements;

namespace Runtime.UI.InGameMenu.SaveMenu
{
    public class SaveMenuView
    {
        public VisualElement Root { get; }
        public TextField SaveNameField { get; }
        public Button SaveButton { get; }

        public SaveMenuView(VisualTreeAsset asset)
        {
            Root = asset.CloneTree().Q<VisualElement>("save-content");
            
            SaveNameField = Root.Q<TextField>("save-name-field");
            SaveButton = Root.Q<Button>("save-button");
        }
    }
}
