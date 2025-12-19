using Runtime.ViewDescriptions.Buildings;
using Runtime.ViewDescriptions.Inventory;

namespace Runtime.ViewDescriptions
{
    public class ViewDescriptions
    {
        public BuildingViewDescriptionCollection BuildingViewDescriptions { get; set; }

        public InventoryViewDescription InventoryViewDescription { get; set; }
    }
}