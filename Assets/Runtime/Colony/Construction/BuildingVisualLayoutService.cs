using Runtime.ViewDescriptions.Buildings;
using UnityEngine;

namespace Runtime.Colony.Construction
{
    public static class BuildingVisualLayoutService
    {
        public static Vector3 GetScale(
            BuildingViewDescription description,
            Bounds bounds,
            float cellSize)
        {
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
                description.VisualSizeInCells.x * cellSize,
                0f,
                description.VisualSizeInCells.y * cellSize
            );

            var additionalScaled = Vector3.Scale(
                description.AdditionalWorldOffset,
                new Vector3(description.VisualSizeInCells.x * cellSize, 1f,
                    description.VisualSizeInCells.y * cellSize)
            );

            return additionalScaled + footprintWorldSize;
        }
    }
}