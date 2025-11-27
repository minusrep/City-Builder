using System;

namespace Runtime.Colony.Buildings.Descriptions
{
    [Serializable]
    public class ProductionBuildingDescription : BuildingDescription
    {
        public float ProductionTime;
        public string ProductionResource;
        public int MaxResource;
    }
}