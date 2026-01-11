using System;
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
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}