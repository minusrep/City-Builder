using Runtime.ViewDescriptions.Buildings;
using Runtime.ViewDescriptions.Inventory;
using Runtime.ViewDescriptions.Stats;

namespace Runtime.ViewDescriptions
{
    public class WorldViewDescriptions
    {
        public BuildingViewDescriptionCollection BuildingViewDescriptions { get; set; }

        public InventoryViewDescription InventoryViewDescription { get; set; }
        
        public StatViewDescriptionCollection StatViewDescriptions { get; set; }
    }
}