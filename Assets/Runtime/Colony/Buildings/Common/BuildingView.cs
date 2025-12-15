using UnityEngine;

namespace Runtime.Colony.Buildings.Common
{
    public class BuildingView : MonoBehaviour
    {
        public Transform Transform { get; private set; }
        public GameObject GameObject { get; private set; }

        public virtual void Initialize()
        {
            Transform = transform;
            GameObject =  gameObject;
        }
    }
}