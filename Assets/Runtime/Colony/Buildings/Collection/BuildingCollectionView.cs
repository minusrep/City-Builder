using UnityEngine;

namespace Runtime.Colony.Buildings.Collection
{
    public sealed class BuildingCollectionView : MonoBehaviour
    {
        public Transform Transform { get; private set; }

        private void Awake()
        {
            Transform = transform;
        }
    }
}