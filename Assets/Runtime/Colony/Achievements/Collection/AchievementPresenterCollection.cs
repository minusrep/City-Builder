using System.Collections.Generic;
using Runtime.Common;
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
                CreateAchievementPresenter(id, model);
            }
        }

        private void CreateAchievementPresenter(string id, AchievementModel model)
        {
            var view = new AchievementView(_viewDescriptions.AchievementsViewDescription.AchievementAsset);

            var achievementPresenter = new AchievementPresenter(model, view, _content);

            _presenters.Add(id, achievementPresenter);

            achievementPresenter.Enable();
        }

        public void Disable()
        {
            foreach (var presenter in _presenters.Values)
            {
                presenter.Disable();
            }
        }
    }
}