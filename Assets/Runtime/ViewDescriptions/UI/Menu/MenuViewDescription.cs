using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.ViewDescriptions.UI.Menu
{
    [CreateAssetMenu(fileName = "Menu", menuName = "City Builder/View Descriptions/UI/Menu")]
    public class MenuViewDescription : ScriptableObject
    {
        public VisualTreeAsset InGameMenuAsset;
        public VisualTreeAsset LoadAsset;
        public VisualTreeAsset LoadPanelAsset;
        public VisualTreeAsset SaveAsset;
        public VisualTreeAsset AchievementsMenuAsset;
    }
}