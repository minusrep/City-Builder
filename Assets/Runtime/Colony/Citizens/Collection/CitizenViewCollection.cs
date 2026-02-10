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

        public void Clear()
        {
            for (int i = Transform.childCount - 1; i >= 0; i--)
            {
                Destroy(Transform.GetChild(i).gameObject);
            }
        }
    }
}