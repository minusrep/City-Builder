using System.Collections.Generic;
using UnityEngine;

namespace Runtime.Colony.Buildings.Common
{
    public class BuildingPreview : MonoBehaviour
    {
        public GameObject GameObject { get; private set; }
        
        [SerializeField] private Renderer[] _renderers;

        public IReadOnlyList<Renderer> Renderers => _renderers;

        private void Awake()
        {
            GameObject = gameObject;
        }
    }
}