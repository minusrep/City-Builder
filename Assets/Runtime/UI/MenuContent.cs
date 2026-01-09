using UnityEngine.UIElements;

namespace Runtime.UI
{
    public class MenuContent
    {
        public VisualElement HudLayer { get; }
        public VisualElement MenuRoot {get; private set;}
        
        public MenuContent(UIDocument menuDocument)
        {
            var root = menuDocument.rootVisualElement;
            MenuRoot = root.Q<VisualElement>("content");
            HudLayer = root.Q<VisualElement>("hud-layer");
        }
    }
}