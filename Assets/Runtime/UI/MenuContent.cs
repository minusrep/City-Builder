using UnityEngine.UIElements;

namespace Runtime.UI
{
    public class MenuContent
    {
        public VisualElement MenuRoot {get; private set;}
        
        public MenuContent(UIDocument menuDocument, UIDocument worldDocument)
        {
            MenuRoot = menuDocument.rootVisualElement.Q<VisualElement>("content");
        }
    }
}