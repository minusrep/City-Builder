using System;
using Runtime.Colony.Achievements.Triggers.Types;
using Runtime.Descriptions.Achievements;

namespace Runtime.Colony.Achievements.Triggers
{
    public class TriggerFactory
    {
        public static TriggerModel Create(TriggerDescription description, AchievementModel model)
        {
            return description.Key switch
            {
                "resource" => new ResourceTriggerModel(description, model),
                "building" => new BuildingTriggerModel(description, model),
                "building_upgrade" => new BuildingUpdateTriggerModel(description, model),
                "building_max_level_count" => new BuildingMaxLevelCountTriggerModel(description, model),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}