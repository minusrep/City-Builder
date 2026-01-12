using Runtime.Descriptions.Achievements;

namespace Runtime.Colony.Achievements.Events
{
    public class AchievementCompleteEvent : GameEvent
    {
        public override string Type => "achievement_complete";
        
        public readonly AchievementDescription Description;

        public AchievementCompleteEvent(AchievementDescription description)
        {
            Description = description;
        }
    }
}