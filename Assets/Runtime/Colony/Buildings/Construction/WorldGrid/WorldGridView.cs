using UnityEngine;

namespace Runtime.Colony.Buildings.Construction.WorldGrid
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class WorldGridView : MonoBehaviour
    {
        public Transform Transform { get; private set; }
        public GameObject GameObject { get; private set; }

        [SerializeField] private MeshFilter _filter;

        private void Awake()
        {
            Transform = transform;
            GameObject = gameObject;
        }
    }
}