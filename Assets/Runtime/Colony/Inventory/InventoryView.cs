using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.Colony.Inventory
{
    public class InventoryView : MonoBehaviour
    {
        public VisualTreeAsset CellAsset;
        public UIDocument Document;
        public VisualElement Root { get; private set; }
        
        public void Awake()
        {
            Root = Document.rootVisualElement.Q<VisualElement>("content");
        }
    }
}