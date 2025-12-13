using Runtime.ViewDescriptions.Buildings;
using Runtime.ViewDescriptions.Inventory;
using UnityEngine;

namespace Runtime.ViewDescriptions
{
    public class ViewDescriptions
    {
        public BuildingViewDescriptionCollection BuildingViewDescriptions { get; } = Resources.Load<BuildingViewDescriptionCollection>(
            "ViewDescriptions/Buildings/BuildingViewDescriptionCollection");

        public InventoryViewDescription InventoryViewDescription { get; } = Resources.Load<InventoryViewDescription>(
            "ViewDescriptions/Items/InventoryViewDescription");
    }
}