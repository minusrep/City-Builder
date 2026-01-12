using System.Collections.Generic;
using Runtime.Colony.Achievements.Events;
using Runtime.Common;
using Runtime.Services;
using Runtime.UI;
using Runtime.ViewDescriptions;

namespace Runtime.Colony.Achievements.Collection
{
    public class AchievementPresenterCollection : IPresenter
    {
        private readonly Dictionary<string, AchievementPresenter> _presenters = new();
        private readonly AchievementModelCollection _model;
        private readonly WorldViewDescriptions _viewDescriptions;
        private readonly MenuContent _content;

        public AchievementPresenterCollection(AchievementModelCollection model, WorldViewDescriptions viewDescriptions,
            MenuContent content)
        {
            _model = model;
            _viewDescriptions = viewDescriptions;
            _content = content;
        }

        public void Enable()
        {
            foreach (var (id, model) in _model.Models)
            {
                CreatePresenter(id, model);
            }
            
            MessageBroker.Instance.Subscribe("achievement_complete", Complete);
        }
        
        public void Disable()
        {
            MessageBroker.Instance.Unsubscribe("achievement_complete", Complete);
            
            foreach (var presenter in _presenters.Values)
            {
                presenter.Disable();
            }
        }

        private void CreatePresenter(string id, AchievementModel model)
        {
            var view = new AchievementView(_viewDescriptions.AchievementsViewDescription.AchievementAsset);
            var achievementPresenter = new AchievementPresenter(model, view, _content, _viewDescriptions);

            _presenters.Add(id, achievementPresenter);
            
            if (model.IsActive)
            {
                achievementPresenter.Enable();
            }
        }

        private void Complete(GameEvent gameEvent)
        {
            if (gameEvent is not AchievementCompleteEvent achievementCompleteEvent)
            {
                return;
            }

            foreach (var achievement  in achievementCompleteEvent.Description.Unlocks)
            {
                _presenters.TryGetValue(achievement, out var presenter);
                
                presenter?.Enable();
            }
        }
    }
}