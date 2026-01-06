using UnityEngine;

namespace Runtime.Colony.Construction
{
    public class BuildingConstructionView : MonoBehaviour
    {
        public Transform Transform { get; private set; }
        public GameObject GameObject { get; private set; }
        public Renderer PreviewRenderer { get; private set; }

        private void Awake()
        {
            Transform = transform;
            GameObject = gameObject;
        }
        
        public void SetPreviewRenderer(Renderer previewRenderer)
        {
            PreviewRenderer = previewRenderer;
            var rendererTransform = previewRenderer.transform;
            rendererTransform.SetParent(Transform);
            rendererTransform.localPosition = Vector3.zero;
            rendererTransform.localRotation = Quaternion.identity;
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