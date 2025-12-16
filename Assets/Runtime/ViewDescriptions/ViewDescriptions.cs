using Runtime.ViewDescriptions.Buildings;
using Runtime.ViewDescriptions.Inventory;

namespace Runtime.ViewDescriptions
{
    public class ViewDescriptions
    {
        public BuildingViewDescriptionCollection BuildingViewDescriptions { get; }

        public InventoryViewDescription InventoryViewDescription { get; }

        public ViewDescriptions(BuildingViewDescriptionCollection buildingViewDescriptionCollection,
            InventoryViewDescription inventoryViewDescription)
        {
            BuildingViewDescriptions = buildingViewDescriptionCollection;
            InventoryViewDescription = inventoryViewDescription;
        }
    }
}