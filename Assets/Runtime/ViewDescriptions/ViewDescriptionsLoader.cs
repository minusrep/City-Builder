using Runtime.ViewDescriptions.Buildings;
using Runtime.ViewDescriptions.Inventory;
using UnityEngine;

namespace Runtime.ViewDescriptions
{
    public static class ViewDescriptionsLoader
    {
        public static ViewDescriptions Load()
        {
            var buildingViews =
                Resources.Load<BuildingViewDescriptionCollection>(
                    "ViewDescriptions/Buildings/BuildingViewDescriptionCollection");

            var inventoryViews =
                Resources.Load<InventoryViewDescription>(
                    "ViewDescriptions/Items/InventoryViewDescription");

            return new ViewDescriptions(buildingViews, inventoryViews);
        }
    }
}