using UnityEngine.UIElements;

namespace Runtime.UI
{
    public class MenuContent
    {
        public VisualElement MenuRoot {get; private set;}
        public VisualElement PopupRoot {get; private set;}
        
        public MenuContent(UIDocument menuDocument, UIDocument popupDocument)
        {
            MenuRoot = menuDocument.rootVisualElement.Q<VisualElement>("content");
            PopupRoot = popupDocument.rootVisualElement.Q<VisualElement>("content");
        }
    }
}