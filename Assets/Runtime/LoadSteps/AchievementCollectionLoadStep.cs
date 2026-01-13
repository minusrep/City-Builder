using System.Collections.Generic;
using System.Threading.Tasks;
using Runtime.Colony;
using Runtime.Colony.Achievements.Collection;
using Runtime.Common;
using Runtime.UI;
using Runtime.ViewDescriptions;

namespace Runtime.LoadSteps
{
    public class AchievementCollectionLoadStep : IStep
    {
        private readonly List<IPresenter> _presenters;
        private readonly World _world;
        private readonly WorldViewDescriptions _worldViewDescriptions;
        private readonly MenuContent _menuContent;

        public AchievementCollectionLoadStep(List<IPresenter> presenters, World world,
            WorldViewDescriptions worldViewDescriptions, MenuContent menuContent)
        {
            _presenters = presenters;
            _world = world;
            _worldViewDescriptions = worldViewDescriptions;
            _menuContent = menuContent;
        }

        public async Task Run()
        {
            var achievementPresenterCollection =
                new AchievementPresenterCollection(_world.Achievements, _worldViewDescriptions, _menuContent);

            achievementPresenterCollection.Enable();

            _presenters.Add(achievementPresenterCollection);

            await Task.CompletedTask;
        }
    }
}