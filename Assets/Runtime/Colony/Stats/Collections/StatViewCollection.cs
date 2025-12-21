using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.Colony.Stats.Collections
{
    public class StatViewCollection : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;
        
        public VisualElement Root => _uiDocument.rootVisualElement;
    }
}