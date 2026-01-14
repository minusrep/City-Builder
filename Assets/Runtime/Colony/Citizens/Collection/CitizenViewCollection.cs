using UnityEngine;

namespace Runtime.Colony.Citizens.Collection
{
    public class CitizenViewCollection : MonoBehaviour
    {
        public Transform Transform { get; private set; }

        private void Awake()
        {
            Transform = transform;
        }
    }
}