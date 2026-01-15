using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.ViewDescriptions.UI.BuildingConstruction
{
    [CreateAssetMenu(fileName = "BuildingConstructionMenuViewDescription",
        menuName = "ViewDescription/BuildingConstruction/BuildingConstructionMenuViewDescription")]
    public class BuildingConstructionMenuViewDescription : ScriptableObject
    {
        public VisualTreeAsset BuildingPanelAsset;
        public VisualTreeAsset ConstructionMenuAsset;
    }
}