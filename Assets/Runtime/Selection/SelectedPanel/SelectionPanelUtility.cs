using System;
using UnityEngine.UIElements;

namespace Runtime.Colony.Buildings.Selection.BuildingPanel
{
    public static class SelectionPanelUtility
    {
        private const string SelectionUpgradeButtonStyleKey = "selection-panel-upgrade-button";
        private const string SelectionPanelFieldStyleKey = "selection-panel-field";
        private const string SelectionTextTitleStyleKey = "selection-panel-title";
        private const string SelectionPanelEnabledStyleKey = "selection-panel-enabled";

        public static TextElement CreateTitle(string value)
        {
            var title = CreateTextElement(SelectionTextTitleStyleKey);

            title.text = value;
            
            return title;
        }

        public static TextElement CreateField<T>(string name, T value)
        {
            var field = CreateTextElement(SelectionPanelFieldStyleKey);

            field.text = $"<color=#FFD700>{name}</color>{value.ToString()}";

            return field;
        }

        private static TextElement CreateTextElement(string style)
        {
            var textElement = new TextElement();
            
            textElement.AddToClassList(style);
            
            return textElement;
        }

        public static Button CreateButton<T>(string name, T value, Action onClick)
        {
            var upgradeButton = new Button()
            {
                enableRichText = true,
                text = $"{name}<color=#FFD700>{value.ToString()}"
            };

            upgradeButton.clicked += () =>
            {
                onClick?.Invoke();
            };
                
            upgradeButton.AddToClassList(SelectionUpgradeButtonStyleKey);
                
            return upgradeButton;
        }

        public static void SetupPanel(VisualElement root)
        {
            root.AddToClassList(SelectionPanelEnabledStyleKey);

            root.Clear();
        }

        public static void HidePanel(VisualElement root)
        {
            root.RemoveFromClassList(SelectionPanelEnabledStyleKey);
        }
    }
}