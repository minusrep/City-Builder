using Runtime.Colony.Achievements.Events;
using Runtime.Colony.Achievements.Events.Types;
using Runtime.Descriptions.Achievements;

namespace Runtime.Colony.Achievements.Triggers.Types
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

        protected override void OnEventReceived(GameEvent gameEvent)
        {
            if (!Model.IsActive)
            {
                return;
            }

            if (gameEvent is not BuildingChangeEvent buildingChangeEvent)
            {
                return;
            }

            if (Description.Value == buildingChangeEvent.Building.Id)
            {
                Model.AddProgress(buildingChangeEvent.Amount);
            }
        }
    }
}