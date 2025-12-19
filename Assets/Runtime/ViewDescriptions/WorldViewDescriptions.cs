using Runtime.ViewDescriptions.Buildings;
using Runtime.ViewDescriptions.Inventory;

namespace Runtime.ViewDescriptions
{
    public class WorldViewDescriptions
    {
        public BuildingViewDescriptionCollection BuildingViewDescriptions { get; set; }

        public InventoryViewDescription InventoryViewDescription { get; set; }
    }
}