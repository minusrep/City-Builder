using Runtime.Common;

namespace Runtime.UI.InGameMenu.AchievementsMenu
{
    public class AchievementsMenuPresenter : IPresenter
    {
        private readonly AchievementsMenuView _view;
        
        public AchievementsMenuPresenter(AchievementsMenuView view)
        {
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