using System;
using Runtime.Descriptions.Buildings;
using UnityEngine;

namespace Runtime.Colony.Buildings
{
    [Serializable]
    public class DecorBuildingModel : BuildingModel
    {
        public DecorBuildingDescription Description;

        public DecorBuildingModel(int id, Vector2 position, DecorBuildingDescription description) : base(id, position)
        {
            Description = description;
        }
    }
}