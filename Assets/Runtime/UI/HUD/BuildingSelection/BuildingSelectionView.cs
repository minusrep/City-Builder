using UnityEngine;

namespace Runtime.UI.HUD.BuildingSelection
{
    public class BuildingSelectionView : MonoBehaviour
    {
        public Camera Camera => _camera;

        [SerializeField] private Camera _camera;
    }
}