using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.Colony.Inventory
{
    public class InventoryView : MonoBehaviour
    {
        public VisualTreeAsset CellAsset;
        public VisualElement Root { get; private set; }
        
        private UIDocument _document;

        public void Awake()
        {
            _document = GetComponent<UIDocument>();
            Root = _document.rootVisualElement.Q<VisualElement>("content");
        }
    }
}