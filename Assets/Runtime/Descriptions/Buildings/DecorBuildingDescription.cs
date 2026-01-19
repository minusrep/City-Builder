using System.Collections.Generic;
using Runtime.Extensions;
using UnityEngine;

namespace Runtime.Descriptions.Buildings
{
    public class DecorBuildingDescription : BuildingDescription
    {
        private const string CircularPointsKey = "circular_points";
        private const string CircularRadiusKey = "circular_radius";
        
        public DecorBuildingDescription(string id, Dictionary<string, object> data) : base(id, data)
        {
            var pointCount = data.GetInt(CircularPointsKey);
            var radius = data.GetFloat(CircularRadiusKey);
            
            InteractionPoints.Clear();
            
            GenerateCircularPoints(pointCount, radius);
        }
        
        private void GenerateCircularPoints(int count, float radius)
        {
            var angleStep = 360f / count;
            var centerX = Size.x * 0.5f * WorldGridDescription.CellSize;
            var centerZ = Size.y * 0.5f * WorldGridDescription.CellSize;
            for (var i = 0; i < count; i++)
            {
                var angle = i * angleStep * Mathf.Deg2Rad;
                var x = centerX + Mathf.Cos(angle) * radius * WorldGridDescription.CellSize;
                var z = centerZ + Mathf.Sin(angle) * radius * WorldGridDescription.CellSize;
                
                InteractionPoints.Add(new Vector3(x, 0, z));
            }
        }
    }
}