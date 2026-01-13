using UnityEngine.UIElements;

namespace Runtime.UI
{
    public class MenuContent
    {
        public VisualElement HudLayer { get; }
        public VisualElement PopupRoot {get; private set;}
        public VisualElement MenuRoot {get; private set;}
        
        public MenuContent(UIDocument menuDocument, UIDocument popupDocument)
        {
            var root = menuDocument.rootVisualElement;
            MenuRoot = root.Q<VisualElement>("content");
            HudLayer = root.Q<VisualElement>("hud-layer");
            
            PopupRoot = popupDocument.rootVisualElement.Q<VisualElement>("content");
        }
    }
}