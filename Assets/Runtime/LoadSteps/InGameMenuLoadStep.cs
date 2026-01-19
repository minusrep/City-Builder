using System.Collections.Generic;
using System.Threading.Tasks;
using Runtime.Colony;
using Runtime.Common;
using Runtime.UI;
using Runtime.UI.InGameMenu;
using Runtime.ViewDescriptions;

namespace Runtime.LoadSteps
{
    public class InGameMenuLoadStep : IStep
    {
        private readonly List<IPresenter> _presenters;
        private readonly World _world;
        private readonly WorldViewDescriptions _worldViewDescriptions;
        private readonly MenuContent _menuContent;

        public InGameMenuLoadStep(List<IPresenter> presenters, World world, WorldViewDescriptions worldViewDescriptions,
            MenuContent menuContent)
        {
            _world = world;
            _worldViewDescriptions = worldViewDescriptions;
            _menuContent = menuContent;
            _presenters = presenters;
        }

        public Task Run()
        {
            var inGameMenuView = new InGameMenuView(_worldViewDescriptions.MenuViewDescription.InGameMenuAsset);
            var inGameMenuPresenter = new InGameMenuPresenter(inGameMenuView, _menuContent, _world,
                _worldViewDescriptions, null);
            inGameMenuPresenter.Enable();
            _presenters.Add(inGameMenuPresenter);
            return Task.CompletedTask;
        }
    }
}