using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.ViewDescriptions.UI.Building
{
    [CreateAssetMenu(fileName = "Selection",
        menuName = "City Builder/View Descriptions/UI/Unit Selection")]
    public class SelectionViewDescription : ScriptableObject
    {
        public VisualTreeAsset SelectionPanelAsset;

        public VisualTreeAsset ConstructionMenuAsset;
    }
}