using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Achievements;
using Runtime.Colony.Achievements.Events;
using Runtime.Colony.Achievements.Events.Types;
using Runtime.Common;
using Runtime.Services;
using Runtime.ViewDescriptions;

namespace Runtime.UI.InGameMenu.AchievementsMenu
{
    public class AchievementsMenuPresenter : IPresenter
    {
        private readonly AchievementsMenuView _view;
        private readonly World _world;
        private readonly WorldViewDescriptions _viewDescriptions;
        private readonly Dictionary<string, AchievementView> _achievements = new();

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
                CreateAchievement(pair.Key, pair.Value);
            }
            
            MessageBroker.Instance.Subscribe("achievement_complete", Update);
        }

        private void CreateAchievement(string id, AchievementModel model)
        {
            var achievement = _view.DrawAchievement(_viewDescriptions);
            var achievementViewDescription = _viewDescriptions.AchievementsViewDescription.Get(id);

            achievement.Icon.style.backgroundImage = achievementViewDescription.Icon.texture;
            achievement.Title.text = achievementViewDescription.Title;
            achievement.Description.text = achievementViewDescription.Description;

            achievement.SetCompletedState(model.IsCompleted);
            achievement.Show();

            _view.Container.Add(achievement.Root);
            _achievements.Add(id, achievement);
        }
        
        private void Update(GameEvent gameEvent)
        {
            if (gameEvent is not AchievementCompleteEvent achievementCompleteEvent)
            {
                return;
                
            }
            _achievements.TryGetValue(achievementCompleteEvent.Description.Id, out var achievementView);
                
            achievementView?.SetCompletedState(true);
        }

        public void Disable()
        {
            MessageBroker.Instance.Unsubscribe("achievement-complete", Update);

            _view.Root.RemoveFromHierarchy();
            _achievements.Clear();
        }
    }
}