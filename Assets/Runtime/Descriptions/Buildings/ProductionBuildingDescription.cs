using System;
using System.Collections.Generic;

namespace Runtime.Descriptions.Buildings
{
    public class ProductionBuildingDescription : BuildingDescription
    {
        public long ProductionTime { get; }
        public string ProductionResource { get; }
        public int ProductionAmount { get; }
        public int MaxResource { get; }

        public ProductionBuildingDescription(Dictionary<string, object> data) : base(data)
        {
            ProductionTime = (long)data["production_time"];
            ProductionResource = (string)data["production_resource"];
            ProductionAmount = Convert.ToInt32(data["production_amount"]);
            MaxResource = Convert.ToInt32(data["max_resource"]);
        }
    }
}