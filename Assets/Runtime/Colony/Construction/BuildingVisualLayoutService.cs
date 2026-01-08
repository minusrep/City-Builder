using System.Collections.Generic;
using Runtime.ViewDescriptions.Buildings;
using UnityEngine;

namespace Runtime.Colony.Construction
{
    public static class BuildingVisualLayoutService
    {
        public static Vector3 GetScale(
            BuildingViewDescription description,
            IReadOnlyList<Renderer> renderers,
            float cellSize)
        {
            var bounds = CalculateBounds(renderers);
            
            var targetSize = new Vector3(
                description.VisualSizeInCells.x * cellSize,
                bounds.size.y,
                description.VisualSizeInCells.y * cellSize
            );

            var currentSize = bounds.size;

            var scale = Mathf.Min(
                targetSize.x / currentSize.x,
                targetSize.z / currentSize.z
            );

            return Vector3.one * scale;
        }

        public static Vector3 GetOffset(
            BuildingViewDescription description,
            float cellSize)
        {
            var footprintWorldSize = new Vector3(
                description.VisualSizeInCells.x * cellSize * 0.5f,
                0f,
                description.VisualSizeInCells.y * cellSize * 0.5f
            );

            var additionalScaled = Vector3.Scale(
                description.AdditionalWorldOffset,
                new Vector3(description.VisualSizeInCells.x * cellSize, 1f,
                    description.VisualSizeInCells.y * cellSize)
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