using Runtime.Colony.Achievements.Events;
using Runtime.Colony.Achievements.Events.Types;
using Runtime.Descriptions.Achievements;

namespace Runtime.Colony.Achievements.Triggers.Types
{
    public class ResourceTriggerModel : TriggerModel
    {
        public ResourceTriggerModel(TriggerDescription description, AchievementModel model) : base(description, model)
        {
        }

        public override void Subscribe()
        {
            MessageBroker.Instance.Subscribe("resource_amount_changed", OnEventReceived);
        }

        public override void Unsubscribe()
        {
            MessageBroker.Instance.Unsubscribe("resource_amount_changed", OnEventReceived);
        }

        protected override void OnEventReceived(GameEvent gameEvent)
        {
            if (!Model.IsActive)
            {
                return;
            }

            if (gameEvent is not ResourceChangeEvent resourceChangeEvent)
            {
                return;
            }
            
            if (Description.Value == resourceChangeEvent.Resource.Id)
            {
                Model.AddProgress(resourceChangeEvent.Amount);
            }
        }
    }
}