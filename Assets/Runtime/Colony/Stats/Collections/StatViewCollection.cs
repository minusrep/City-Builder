using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

namespace Runtime.Colony.Stats.Collections
{
    public class StatViewCollection : MonoBehaviour
    {
        [FormerlySerializedAs("_uiDocument")] public UIDocument UiDocument;
        
        public VisualElement Root => UiDocument.rootVisualElement;
    }
}