using Runtime.ViewDescriptions.Stats;
using UnityEngine.UIElements;

namespace Runtime.Colony.Stats
{
    public class StatView
    {
        private readonly ProgressBar _bar;
        
        public VisualElement Root { get; }

        public float Value
        {
            get => _bar.value;
            set => _bar.value = value;
        }

        public StatView(StatViewDescription description)
        {
            Root = description.StatViewAsset.CloneTree().Q<VisualElement>("stat");
            Root.styleSheets.Add(description.StyleSheet);
            
            _bar = Root.Q<ProgressBar>("stat-bar");
            
            var icon = Root.Q<VisualElement>("stat-icon");
            icon.style.backgroundImage = description.Icon.texture;
        }
    }
}