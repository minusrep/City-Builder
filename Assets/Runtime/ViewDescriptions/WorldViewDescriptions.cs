using Runtime.ViewDescriptions.Achievements;
using Runtime.ViewDescriptions.Buildings;
using Runtime.ViewDescriptions.Citizens;
using Runtime.ViewDescriptions.Inventory;
using Runtime.ViewDescriptions.Stats;

namespace Runtime.ViewDescriptions
{
    public class WorldViewDescriptions
    {
        public BuildingViewDescriptionCollection BuildingViewDescriptions { get; set; }
        
        public CitizenViewDescription CitizenViewDescription { get; set; }

        public InventoryViewDescription InventoryViewDescription { get; set; }

        public StatViewDescriptionCollection StatViewDescriptions { get; set; }

        public AchievementViewDescriptionCollection AchievementsViewDescription { get; set; }
    }
}