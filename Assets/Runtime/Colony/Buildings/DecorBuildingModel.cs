using System.Collections.Generic;
using Runtime.Descriptions.Buildings;
using UnityEngine;

namespace Runtime.Colony.Buildings
{
    public class DecorBuildingModel : BuildingModel
    {
        public DecorBuildingModel(int id,
            Vector2 position,
            DecorBuildingDescription baseDescription) : base(id,
            position,
            baseDescription)
        {
        }

        public override void Deserialize(Dictionary<string, object> data)
        {
        }
    }
}