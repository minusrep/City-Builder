using Runtime.Colony.Achievements.Events;
using Runtime.Common;
using Runtime.Descriptions.Achievements;
using Runtime.UI;
using UnityEngine;

namespace Runtime.Colony.Achievements
{
    public class AchievementPresenter : IPresenter
    {
        private readonly AchievementModel _model;
        private readonly AchievementView _view;
        private readonly MenuContent _content;

        public AchievementPresenter(AchievementModel model, AchievementView view, MenuContent content)
        {
            _model = model;
            _view = view;
        }

        public void Enable()
        {
            foreach (var trigger in _model.Description.Triggers)
            {
                MessageBroker.Instance.Subscribe(trigger.Event, e => OnEvent(e, trigger));
                
                Debug.Log($"Subscribe on GameEvent: {trigger.Event} with target: {trigger.Target}");
            }
        }

        private void OnEvent(GameEvent gameEvent, Trigger trigger)
        {
            Debug.Log($"{trigger.Target} changed");
        }

        public void Disable()
        {
            foreach (var trigger in _model.Description.Triggers)
            {
                MessageBroker.Instance.Unsubscribe(trigger.Event, e => OnEvent(e, trigger));
            }
        }
    }
}