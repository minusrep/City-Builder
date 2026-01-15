using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.ViewDescriptions.UI.Building
{
    [CreateAssetMenu(fileName = "BuildingMenuViewDescription",
        menuName = "ViewDescription/BuildingConstruction/BuildingMenuViewDescription")]
    public class BuildingMenuViewDescription : ScriptableObject
    {
        public VisualTreeAsset BuildingPanelAsset;
        public VisualTreeAsset ConstructionMenuAsset;
    }
}