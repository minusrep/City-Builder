using System.Collections.Generic;
using Runtime.ViewDescriptions.Buildings;
using UnityEngine;

namespace Runtime.Colony.Buildings.Construction
{
    public static class BuildingVisualLayoutHelper
    {
        public static Vector3 GetScale(
            BuildingViewDescription description,
            IReadOnlyList<Renderer> renderers)
        {
            var bounds = CalculateBounds(renderers);

            var targetSize = new Vector3(
                description.VisualSizeInCells.x,
                bounds.size.y,
                description.VisualSizeInCells.y
            );

            var currentSize = bounds.size;

            var scale = Mathf.Min(
                targetSize.x / currentSize.x,
                targetSize.z / currentSize.z
            );

            return Vector3.one * scale;
        }

        public static Vector3 GetOffset(
            BuildingViewDescription description)
        {
            var footprintWorldSize = new Vector3(
                description.VisualSizeInCells.x * 0.5f,
                0f,
                description.VisualSizeInCells.y * 0.5f
            );

            var additionalScaled = Vector3.Scale(
                description.AdditionalWorldOffset,
                new Vector3(description.VisualSizeInCells.x, 1f,
                    description.VisualSizeInCells.y)
            );

            return additionalScaled + footprintWorldSize;
        }

        private static Bounds CalculateBounds(IReadOnlyList<Renderer> renderers)
        {
            var bounds = renderers[0].bounds;

            for (var i = 1; i < renderers.Count; i++)
            {
                bounds.Encapsulate(renderers[i].bounds);
            }

            return bounds;
        }
    }
}