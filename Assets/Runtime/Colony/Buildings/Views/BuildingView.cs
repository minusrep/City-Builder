using UnityEngine;

namespace Runtime.Colony.Buildings.Views
{
    public sealed class BuildingView : MonoBehaviour
    {
        public Transform Transform { get; set; }
        public GameObject GameObject { get; set; }

        private void Awake()
        {
            Transform = transform;
            GameObject =  gameObject;
        }
    }
}