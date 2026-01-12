using System.Threading.Tasks;
using Runtime.Common;
using Runtime.UI;
using Runtime.ViewDescriptions;

namespace Runtime.Colony.Achievements
{
    public class AchievementPresenter : IPresenter
    {
        private readonly AchievementModel _model;
        private readonly AchievementView _view;
        private readonly WorldViewDescriptions _viewDescriptions;
        private readonly MenuContent _content;

        public AchievementPresenter(AchievementModel model, AchievementView view, MenuContent content,
            WorldViewDescriptions viewDescriptions)
        {
            _model = model;
            _view = view;
            _content = content;
            _viewDescriptions = viewDescriptions;
        }

        public void Enable()
        {
            _model.IsActive = true;
            _model.OnCompleted += Show;

            foreach (var trigger in _model.Triggers)
            {
                trigger.Subscribe();
            }
        }

        public void Disable()
        {
            _model.OnCompleted -= Show;

            foreach (var trigger in _model.Triggers)
            {
                trigger.Unsubscribe();
            }
        }

        private async void Show()
        {
            var viewDescription = _viewDescriptions.AchievementsViewDescription.Get(_model.Description.Id);

            _view.Icon.style.backgroundImage = viewDescription.Icon.texture;
            _view.Title.text = viewDescription.Title;
            _view.Description.text = viewDescription.Description;

            _content.PopupRoot.Add(_view.Root);
            
            await Task.Delay(1); //TODO: Без - не работает анимация
            _view.Root.AddToClassList("show");

            await _view.AwaitTransitionAsync();
            _view.Icon.RemoveFromClassList("not-completed");
            _view.Icon.AddToClassList("completed");

            await Task.Delay(_viewDescriptions.AchievementsViewDescription.Duration);
            _view.Root.RemoveFromClassList("show");

            await _view.AwaitTransitionAsync();
            _content.PopupRoot.Clear();
            Disable();
        }
    }
}