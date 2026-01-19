using System.Collections.Generic;
using System.Threading.Tasks;
using Runtime.Colony;
using Runtime.Common;
using Runtime.UI;
using Runtime.UI.DayNightIndicator;
using Runtime.ViewDescriptions;

namespace Runtime.LoadSteps
{
    public class DayNightIndicatorLoadStep : IStep
    {
        private readonly List<IPresenter> _presenters;
        private readonly World _world;
        private readonly MenuContent _menuContent;
        private readonly DayNightIndicatorView _dayNightIndicatorView;

        public DayNightIndicatorLoadStep(List<IPresenter> presenters, World world, WorldViewDescriptions worldViewDescriptions, MenuContent menuContent)
        {
            _presenters = presenters;
            _world = world;
            _menuContent = menuContent;
            _dayNightIndicatorView = new DayNightIndicatorView(worldViewDescriptions.HudViewDescription.DayNightIndicatorAsset);
        }

        public async Task Run()
        {
            var presenter = new DayNightIndicatorPresenter(_dayNightIndicatorView, _world, _menuContent);
            presenter.Enable();
            _presenters.Add(presenter);
            
            await Task.CompletedTask;
        }
    }
}