using UnityEngine;

namespace Runtime.Colony.Buildings.Views
{
    public sealed class BuildingView : MonoBehaviour
    {
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}