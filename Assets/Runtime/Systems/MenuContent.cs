using UnityEngine.UIElements;

namespace Runtime.Systems
{
    public class MenuContent
    {
        public VisualElement MenuRoot {get; private set;}
        
        public MenuContent(UIDocument document)
        {
            MenuRoot = document.rootVisualElement.Q<VisualElement>("content");
        }
    }
}