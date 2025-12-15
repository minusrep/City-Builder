using UnityEngine;

namespace Runtime.Colony.Buildings.Common
{
    public class BuildingView : MonoBehaviour
    {
        public Transform Transform { get; private set; }
        public GameObject GameObject { get; private set; }

        public void Initialize()
        {
            Transform = transform;
            GameObject =  gameObject;
        }
    }
}