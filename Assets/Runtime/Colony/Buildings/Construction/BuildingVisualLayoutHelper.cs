using System;
using Runtime.ViewDescriptions.Buildings;
using UnityEngine;

namespace Runtime.Colony.Buildings.Construction
{
    public static class BuildingVisualLayoutHelper
    {
        public static Vector3 GetScale(BuildingViewDescription description)
        {
            var targetWidth = description.VisualSizeInCells.x;
            var targetHeight = description.VisualSizeInCells.y;

            var scale = Math.Min(targetWidth, targetHeight);
            return Vector3.one * scale;
        }
    }
}