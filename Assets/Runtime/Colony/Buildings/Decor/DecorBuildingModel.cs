using System.Collections.Generic;
using Runtime.Colony.Buildings.Common;
using Runtime.Descriptions.Buildings;
using UnityEngine;

namespace Runtime.Colony.Buildings.Decor
{
    public class DecorBuildingModel : BuildingModel
    {
        public DecorBuildingModel(string id,
            Vector2Int gridPosition,
            DecorBuildingDescription baseDescription) : base(id,
            gridPosition,
            baseDescription)
        {
        }

        public override void Deserialize(Dictionary<string, object> data)
        {
        }
    }
}