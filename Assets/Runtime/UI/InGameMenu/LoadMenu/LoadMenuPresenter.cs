using Runtime.Common;

namespace Runtime.UI.InGameMenu.LoadMenu
{
    public class LoadMenuPresenter : IPresenter
    {
        private readonly LoadMenuView _view;

        public LoadMenuPresenter(LoadMenuView view)
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