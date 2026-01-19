using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.ViewDescriptions.UI
{
    [CreateAssetMenu(fileName = "Hud",
        menuName = "City Builder/View Descriptions/UI/Hud")]
    public class HudViewDescription : ScriptableObject
    {
        public VisualTreeAsset SelectionPanelAsset;

        public VisualTreeAsset ConstructionMenuAsset;
        
        public VisualTreeAsset DayNightIndicatorAsset;
    }
}