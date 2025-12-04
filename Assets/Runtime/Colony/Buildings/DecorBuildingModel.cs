using Runtime.Descriptions.Buildings;
using UnityEngine;

namespace Runtime.Colony.Buildings
{
    public class DecorBuildingModel : BuildingModel
    {
        public DecorBuildingModel(int id,
            Vector2 position,
            DecorBuildingDescription description) : base(id,
            position,
            description)
        {
        }
    }
}