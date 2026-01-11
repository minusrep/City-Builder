using Runtime.Colony.Achievements.Events;
using Runtime.Descriptions.Achievements;

namespace Runtime.Colony.Achievements.Triggers
{
    public abstract class TriggerModel
    {
        public TriggerDescription Description { get; }
        public AchievementModel Model { get; }

        public TriggerModel(TriggerDescription description, AchievementModel model)
        {
            Description = description;
            Model = model;
        }

        public abstract void Subscribe();
        public abstract void Unsubscribe();
        protected abstract void OnEventReceived(GameEvent evt);
    }
}