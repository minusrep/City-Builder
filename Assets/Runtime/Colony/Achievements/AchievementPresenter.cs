using Runtime.Common;
using Runtime.UI;

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
        }

        public void Disable()
        {

        }
    }
}