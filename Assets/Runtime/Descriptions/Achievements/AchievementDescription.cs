using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Descriptions.Achievements
{
    public class AchievementDescription
    {
        public string Type { get; }
        public int Target { get; }
        public List<Trigger> Triggers { get; } = new();
        public List<string> Unlocks { get; } = new();

        public AchievementDescription(Dictionary<string, object> description)
        {
            Type = description.GetString("type");
            Target = description.GetInt("target");

            var triggersList = (List<object>)description["triggers"];
            foreach (var triggerObject in triggersList)
            {
                var triggerDict = (Dictionary<string, object>)triggerObject;
                var trigger = new Trigger(triggerDict);
                
                Triggers.Add(trigger);
            }

            if (description.TryGetValue("unlocks", out var value))
            {
                var unlocksList = (List<object>)value;
                foreach (var unlockObject in unlocksList)
                {
                    Unlocks.Add(unlockObject.ToString());
                }
            }
        }
    }
}