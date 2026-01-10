using UnityEngine;

namespace Runtime.Colony.Buildings.Construction.WorldGrid
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class WorldGridView : MonoBehaviour
    {
        public Transform Transform { get; private set; }
        public GameObject GameObject { get; private set; }
        public MeshFilter Filter => _filter;

        [SerializeField] private MeshFilter _filter;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Color _gridColor = Color.grey;

        private void Awake()
        {
            Transform = transform;
            GameObject = gameObject;
            _renderer.material.color = _gridColor;
        }
    }
}