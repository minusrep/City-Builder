using System.Threading.Tasks;
using Runtime.Common;
using Runtime.UI;
using Runtime.ViewDescriptions;
using UnityEngine.UIElements;

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
            _model.OnCompleted += Complete;

            foreach (var trigger in _model.Triggers)
            {
                trigger.Subscribe();
            }
        }

        public void Disable()
        {
            _model.OnCompleted -= Complete;

            foreach (var trigger in _model.Triggers)
            {
                trigger.Unsubscribe();
            }
        }
        
        private async void Complete()
        {
            await ShowAchievementAsync();
            
            Disable();
        }
        
        private async Task ShowAchievementAsync()
        {
            await ShowPopupAsync();
            
            _view.SetCompletedState(true);
            await Task.Delay(_viewDescriptions.AchievementsViewDescription.Duration);
            
            _view.Hide();
            await AwaitTransitionAsync(_view.Root);
            _view.Root.RemoveFromHierarchy();
        }

        private async Task ShowPopupAsync()
        {
            var viewDescription = _viewDescriptions.AchievementsViewDescription.Get(_model.Description.Id);

            _view.Icon.style.backgroundImage = viewDescription.Icon.texture;
            _view.Title.text = viewDescription.Title;
            _view.Description.text = viewDescription.Description;

            _content.PopupRoot.Add(_view.Root);
            await UnityAwaiter.NextFrame();
            
            _view.Show();
            await AwaitTransitionAsync(_view.Root);
        }
        
        private async Task AwaitTransitionAsync(VisualElement element)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            element.RegisterCallback<TransitionEndEvent>(OnEnd);
            await taskCompletionSource.Task;
            return;

            void OnEnd(TransitionEndEvent evt)
            {
                element.UnregisterCallback<TransitionEndEvent>(OnEnd);
                taskCompletionSource.TrySetResult(true);
            }
        }
    }
}