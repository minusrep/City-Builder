using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Runtime.Colony.Achievements
{
    public class AchievementView
    {
        public VisualElement Root { get; }
        public VisualElement Icon { get; }
        public Label Title { get; }
        public Label Description { get; }

        public AchievementView(VisualTreeAsset asset)
        {
            Root = asset.CloneTree().Q<VisualElement>("achievement");
            
            Icon = Root.Q<VisualElement>("icon");
            Title = Root.Q<Label>("title");
            Description = Root.Q<Label>("description");
        }
        
        public Task AwaitTransitionAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            void OnEnd(TransitionEndEvent evt)
            {
                Root.UnregisterCallback<TransitionEndEvent>(OnEnd);
                taskCompletionSource.TrySetResult(true);
            }

            Root.RegisterCallback<TransitionEndEvent>(OnEnd);

            return taskCompletionSource.Task;
        }
    }
}