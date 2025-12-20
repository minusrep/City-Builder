using UnityEngine;

namespace Runtime.UI
{
    public class UIBillboard : MonoBehaviour
    {
        private Camera _camera;

        //TODO: Убрать старт
        void Start()
        {
            _camera = Camera.main;
        }

        //TODO: Камера хранится в системе, закэшировать трансформы
        void LateUpdate()
        {
            transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward,
                _camera.transform.rotation * Vector3.up);
        }
    }
}