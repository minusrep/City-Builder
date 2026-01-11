using Runtime.UI.HUD.BuildingSelection;
using UnityEngine;
using UnityEngine.UIElements;

namespace Runtime.UI.HUD
{
    public class HUDView : MonoBehaviour
    {
        public VisualElement Root => _uiDocument.rootVisualElement;
        public BuildingSelectionView BuildingSelectionView => _buildingSelectionView;

        [SerializeField] private UIDocument _uiDocument;
        
        [SerializeField] private BuildingSelectionView _buildingSelectionView;
        
    }
}