using Runtime.Colony.Achievements.Events;
using Runtime.Descriptions.Achievements;
using Runtime.Services;

namespace Runtime.Colony.Achievements.Triggers
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
            if (Model.IsActive == false)
            {
                return;
            }

            if (gameEvent is ResourceChangeEvent resourceChangeEvent)
            {
                if (Description.Value == resourceChangeEvent.Resource.Id)
                {
                    Model.AddProgress(resourceChangeEvent.Amount);
                }
            }
        }
    }
}