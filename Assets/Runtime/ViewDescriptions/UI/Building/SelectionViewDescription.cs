using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.ViewDescriptions.UI.Building
{
    [CreateAssetMenu(fileName = "SelectionViewDescription",
        menuName = "ViewDescription/SelectionViewDescription")]
    public class SelectionViewDescription : ScriptableObject
    {
        public VisualTreeAsset BuildingPanelAsset;

        public VisualTreeAsset StatAsset;
        
        public VisualTreeAsset ConstructionMenuAsset;
    }
}