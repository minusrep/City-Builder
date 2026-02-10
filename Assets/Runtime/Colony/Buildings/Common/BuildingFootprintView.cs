using UnityEngine;

namespace Runtime.Colony.Buildings.Common
{
    public class BuildingFootprintView : MonoBehaviour
    {
        public GameObject GameObject { get; private set; }
        public Renderer Renderer => _renderer;

        [SerializeField] private Renderer _renderer;
        
        public void Awake()
        {
            GameObject =  gameObject;
            GameObject.SetActive(false);
        }
    }
}