using Runtime.ViewDescriptions.Achievements;
using Runtime.ViewDescriptions.Buildings;
using Runtime.ViewDescriptions.Citizens;
using Runtime.ViewDescriptions.Inventory;
using Runtime.ViewDescriptions.Stats;
using Runtime.ViewDescriptions.UI;
using Runtime.ViewDescriptions.UI.Menu;

namespace Runtime.ViewDescriptions
{
    public class WorldViewDescriptions
    {
        public BuildingViewDescriptionCollection BuildingViewDescriptions { get; set; }
        
        public CitizenViewDescriptionCollection CitizenViewDescriptionCollection { get; set; }

        public InventoryViewDescription InventoryViewDescription { get; set; }

        public StatViewDescriptionCollection StatViewDescriptions { get; set; }

        public AchievementViewDescriptionCollection AchievementsViewDescription { get; set; }
        
        public MenuViewDescription MenuViewDescription { get; set; }
        
        public HudViewDescription HudViewDescription { get; set; }
    }
}