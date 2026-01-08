using System.Collections.Generic;

namespace Runtime.Descriptions.Achievements
{
    public class AchievementDescriptionCollection
    {
        public Dictionary<string, AchievementDescription> Descriptions { get; } = new();

        public AchievementDescriptionCollection(Dictionary<string, object> descriptions)
        {
            foreach (var pair in descriptions)
            {
                var descriptionDict = (Dictionary<string, object>)pair.Value;
                var description = new AchievementDescription(descriptionDict);
                Descriptions.Add(pair.Key, description);
            }
        }
    }
}