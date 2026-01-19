using Runtime.Colony;
using Runtime.Common;

namespace Runtime.UI.DayNightIndicator
{
    public class DayNightIndicatorPresenter : IPresenter
    {
        private readonly DayNightIndicatorView _view;
        private readonly World _world;
        private readonly MenuContent _menuContent;
        private readonly DayNightIndicatorSystem _system;

        public DayNightIndicatorPresenter(DayNightIndicatorView view, World world, MenuContent menuContent)
        {
            _view = view;
            _world = world;
            _menuContent = menuContent;
            _system = new DayNightIndicatorSystem(view, world);
        }

        public void Enable()
        {
            _menuContent.HudLayer.Add(_view.Root);
            _world.GameSystems.Add(_system);
        }

        public void Disable()
        {
            _world.GameSystems.Remove(_system);
            _menuContent.HudLayer.Remove(_view.Root);
        }
    }
}