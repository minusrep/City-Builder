using Runtime.Colony.Buildings.Common;
using UnityEngine;

namespace Runtime.Colony.Buildings.Construction
{
    public class BuildingConstructionView : MonoBehaviour
    {
        public Transform Transform { get; private set; }
        public GameObject GameObject { get; private set; }
        public BuildingPreview Preview { get; set; }
        public bool IsReady;

        private void Awake()
        {
            Transform = transform;
            GameObject = gameObject;
        }

        public void SetValid(bool canPlace)
        {
            var color = canPlace ? Color.green : Color.red;

            if (IsReady)
            {
                foreach (var preview in Preview.Renderers)
                {
                    foreach (var material in preview.materials)
                    {
                        material.color = color;
                    }
                }
            }
        }
    }
}