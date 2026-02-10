using UnityEngine.UIElements;

namespace Runtime.UI.DayNightIndicator
{
    public class DayNightIndicatorView
    {
        public VisualElement Root { get; }
        public VisualElement IndicatorInner { get; }

        public DayNightIndicatorView(VisualTreeAsset dayNightIndicatorAsset)
        {
            Root = dayNightIndicatorAsset.CloneTree().Q<VisualElement>("day-night-indicator");
            IndicatorInner = Root.Q<VisualElement>("indicator-inner");
        }
    }
}