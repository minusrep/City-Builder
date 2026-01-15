using UnityEngine;

namespace Runtime.UI
{
    public class CitizenUIBillboard : MonoBehaviour
    {
        private Transform _cameraTransform;

        private void Start()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.rotation = _cameraTransform.rotation;
        }
    }
}