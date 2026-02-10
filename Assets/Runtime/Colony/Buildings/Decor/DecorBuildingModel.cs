using Runtime.Colony.Buildings.Common;
using Runtime.Descriptions.Buildings;
using UnityEngine;

namespace Runtime.Colony.Buildings.Decor
{
    public class DecorBuildingModel : BuildingModel
    {
        private int _currentPointIndex;
        
        public DecorBuildingModel(string id,
            Vector2Int gridPosition,
            DecorBuildingDescription baseDescription) : base(id,
            gridPosition,
            baseDescription)
        {
        }

        public override Vector3 GetInteractionPoint()
        {
            var point = WorldPosition + BaseDescription.InteractionPoints[_currentPointIndex];
            _currentPointIndex = (_currentPointIndex + 1) % BaseDescription.InteractionPoints.Count;
            return point;
        }
    }
}