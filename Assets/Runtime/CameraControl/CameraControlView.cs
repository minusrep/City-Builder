using Unity.Cinemachine;
using UnityEngine;

namespace Runtime.CameraControl
{
    public class CameraControlView : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private CinemachineOrbitalFollow _orbitalFollow;
        [SerializeField] private Transform _cameraMainTransform;

        public Transform Transform => _transform;
        public CinemachineOrbitalFollow OrbitalFollow => _orbitalFollow;
        public Transform CameraMainTransform => _cameraMainTransform;

    }
}
