using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.Colony.Citizens.HUD
{
    public class CitizenHUDView : MonoBehaviour
    {
        public VisualElement Root => _uiDocument.rootVisualElement;
        
        public UIDocument UIDocument => _uiDocument;
        
        [SerializeField] private UIDocument _uiDocument;
    }
}