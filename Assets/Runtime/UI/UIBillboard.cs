using UnityEngine;

namespace Runtime.UI
{
    public class UIBillboard : MonoBehaviour
    {
        private Camera _camera;

        void Start()
        {
            _camera = Camera.main;
        }

        void LateUpdate()
        {
            transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward,
                _camera.transform.rotation * Vector3.up);
        }
    }
}