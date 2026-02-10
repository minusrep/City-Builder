using UnityEngine;

namespace Runtime.UI
{
    public class UIBillboard : MonoBehaviour
    {
        private Camera _camera;

        //TODO: Убрать старт
        private void Start()
        {
            _camera = Camera.main;
        }

        //TODO: Камера хранится в системе, закэшировать трансформы
        private void LateUpdate()
        {
            transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward,
                _camera.transform.rotation * Vector3.up);
        }
    }
}