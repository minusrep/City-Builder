using Runtime.Colony.Buildings.Common;
using Runtime.ViewDescriptions.Inventory;
using UnityEngine;

namespace Runtime.ViewDescriptions.Buildings
{
    [CreateAssetMenu(fileName = "BuildingViewDescription", menuName = "Buildings/ViewDescription")]
    public class BuildingViewDescription : ScriptableObject
    {
        public BuildingView Prefab;
        public InventoryViewDescription InventoryViewDescription;
    }
}