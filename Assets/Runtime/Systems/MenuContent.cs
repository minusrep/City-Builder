using UnityEngine.UIElements;

namespace Runtime.Systems
{
    public class MenuContent
    {
        public VisualElement MenuRoot {get; private set;}
        public VisualElement WorldRoot {get; private set;}
        
        public MenuContent(UIDocument menuDocument, UIDocument worldDocument)
        {
            MenuRoot = menuDocument.rootVisualElement.Q<VisualElement>("content");
            WorldRoot = worldDocument.rootVisualElement.Q<VisualElement>("content");
        }
    }
}