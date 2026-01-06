using UnityEngine;

namespace Runtime.Colony.Construction
{
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class WorldGridView : MonoBehaviour
    {
        public Transform Transform { get; private set; }
        public GameObject GameObject { get; private set; }
        public MeshFilter Filter { get; private set; }

        private void Awake()
        {
            Transform = transform;
            GameObject = gameObject;
            Filter = GetComponent<MeshFilter>();
        }
    }
}