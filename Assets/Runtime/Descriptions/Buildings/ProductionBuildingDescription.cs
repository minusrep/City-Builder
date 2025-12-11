using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Descriptions.Buildings
{
    public class ProductionBuildingDescription : BuildingDescription
    {
        public long ProductionTime { get; }
        public string ProductionResource { get; }
        public int ProductionAmount { get; }
        public int MaxResource { get; }

        public ProductionBuildingDescription(string id, Dictionary<string, object> data) : base(id, data)
        {
            ProductionTime = data.GetLong("production_time");
            ProductionResource = data.GetString("production_resource");
            ProductionAmount = data.GetInt("production_amount");
            MaxResource = data.GetInt("max_resource");
        }
    }
}