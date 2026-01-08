using UnityEngine;

namespace Runtime.Colony.Construction
{
    public class BuildingConstructionView : MonoBehaviour
    {
        public Transform Transform { get; private set; }
        public GameObject GameObject { get; private set; }
        public Renderer PreviewRenderer { get; set; }

        private void Awake()
        {
            Transform = transform;
            GameObject = gameObject;
        }
        
        public void SetPreviewRenderer(Renderer previewRenderer)
        {
            PreviewRenderer = previewRenderer;
        }
        
        public void SetValid(bool canPlace)
        {
            var materials = PreviewRenderer.materials;
            foreach (var material in materials)
            {
                material.color = canPlace ? Color.green : Color.red;
            }
        }
    }
}