using Unity.Cinemachine;
using UnityEngine;

namespace Runtime.Selection
{
    public class SelectionView : MonoBehaviour
    {
        public Transform Transform { get; private set; }
        public Camera UnitCamera => _unitCamera;
        public CinemachineCamera VirtualCamera => _virtualCamera;
        public Transform UnitCameraTransform  => _unitCameraTransform;

        [SerializeField] private Camera _unitCamera;
        [SerializeField] private Transform _unitCameraTransform;
        [SerializeField] private CinemachineCamera _virtualCamera;

        private void Awake()
        {
            Transform = transform;
        }
    }
}