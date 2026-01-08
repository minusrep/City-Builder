using Runtime.Colony;
using Runtime.Common;
using Runtime.ViewDescriptions;

namespace Runtime.UI.InGameMenu.AchievementsMenu
{
    public class AchievementsMenuPresenter : IPresenter
    {
        private readonly AchievementsMenuView _view;
        private readonly World _world;
        private readonly WorldViewDescriptions _viewDescriptions;
        
        public AchievementsMenuPresenter(AchievementsMenuView view, World world, WorldViewDescriptions viewDescriptions)
        {
            _view = view;
            _world = world;
            _viewDescriptions = viewDescriptions;
        }

        public void Enable()
        {
            foreach (var pair in _world.Achievements.Models)
            {
                var achievement = _view.DrawAchievement(_viewDescriptions);

                var achievementViewDescription = _viewDescriptions.AchievementsViewDescription.Get(pair.Key);
                
                achievement.Icon.style.backgroundImage = achievementViewDescription.Icon.texture;
                achievement.Title.text = achievementViewDescription.Title;
                achievement.Description.text = achievementViewDescription.Description;

                if (pair.Value.IsCompleted)
                {
                    achievement.Icon.RemoveFromClassList("not-completed");
                    achievement.Icon.AddToClassList("completed");
                }
                
                _view.Container.Add(achievement.Root);
            }
        }

        public void Disable()
        {
            _view.Container.Clear();
        }
    }
}