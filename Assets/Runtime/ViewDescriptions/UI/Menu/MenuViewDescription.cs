using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.ViewDescriptions.UI.Menu
{
    [CreateAssetMenu(fileName = "MenuViewDescription", menuName = "ViewDescription/Load/MenuViewDescription")]
    public class MenuViewDescription : ScriptableObject
    {
        public VisualTreeAsset InGameMenuAsset;
        public VisualTreeAsset LoadAsset;
        public VisualTreeAsset SaveAsset;
        public VisualTreeAsset AchievementsMenuAsset;
    }
}