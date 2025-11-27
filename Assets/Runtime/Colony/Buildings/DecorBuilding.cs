using System;
using Runtime.Colony.Buildings.Descriptions;

namespace Runtime.Colony.Buildings
{
    [Serializable]
    public class DecorBuilding : Building
    {
        public DecorBuildingDescription Description;

        public DecorBuilding(DecorBuildingDescription description)
        {
            Description = description;
        }
    }
}