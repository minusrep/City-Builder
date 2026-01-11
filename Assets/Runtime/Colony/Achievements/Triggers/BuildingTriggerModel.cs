using Runtime.Colony.Achievements.Events;
using Runtime.Descriptions.Achievements;
using Runtime.Services;

namespace Runtime.Colony.Achievements.Triggers
{
    public class BuildingTriggerModel : TriggerModel
    {
        public BuildingTriggerModel(TriggerDescription description, AchievementModel model) : base(description, model)
        {
        }

        public override void Subscribe()
        {
            MessageBroker.Instance.Subscribe("buildings_count_changed", OnEventReceived);
        }

        public override void Unsubscribe()
        {
            MessageBroker.Instance.Unsubscribe("buildings_count_changed", OnEventReceived);
        }

        protected override void OnEventReceived(GameEvent evt)
        {
            if (evt is not BuildingChange buildingEvent)
            {
                return;
            }

            if (Description.Value == buildingEvent.Building.Type)
            {
                
            }
        }
    }
}