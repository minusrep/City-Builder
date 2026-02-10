using Runtime.Colony.Achievements.Events;
using Runtime.Colony.Achievements.Events.Types;
using Runtime.Descriptions.Achievements;

namespace Runtime.Colony.Achievements.Triggers.Types
{
    public class BuildingUpdateTriggerModel : TriggerModel
    {
        public BuildingUpdateTriggerModel(TriggerDescription description, AchievementModel model) : base(description, model)
        {
        }

        public override void Subscribe()
        {
            MessageBroker.Instance.Subscribe("building_level_updated", OnEventReceived);
        }

        public override void Unsubscribe()
        {
            MessageBroker.Instance.Unsubscribe("building_level_updated", OnEventReceived);
        }

        protected override void OnEventReceived(GameEvent gameEvent)
        {
            if (!Model.IsActive)
            {
                return;
            }

            if (gameEvent is not BuildingUpdateEvent buildingUpdateEvent)
            {
                return;
            }
            
            if (Description.Value == buildingUpdateEvent.Building.Type)
            {
                Model.AddProgress(buildingUpdateEvent.Level);
            }
        }
    }
}