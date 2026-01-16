using UnityEngine;

namespace Runtime.Selection
{
    public class SelectionView : MonoBehaviour
    {
        public Camera UnitCamera => _unitCamera;
        
        public Transform UnitCameraTransform  => _unitCameraTransform;
        
        [SerializeField] private Camera _unitCamera;
        
        [SerializeField] private Transform _unitCameraTransform;
    }
}