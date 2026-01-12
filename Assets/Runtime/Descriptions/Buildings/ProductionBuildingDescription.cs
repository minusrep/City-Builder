using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Descriptions.Buildings
{
    public class ProductionBuildingDescription : BuildingDescription
    {
        public List<long> ProductionTimeByLevel { get; }
        public string ProductionResource { get; }
        public int ProductionAmount { get; }
        public int MaxResource { get; }
        public Dictionary<string, int> ResourcesForWork { get; }
        public Dictionary<string, int> ResourcesForProduction { get; }

        public ProductionBuildingDescription(string id, Dictionary<string, object> data) : base(id, data)
        {
            ProductionTimeByLevel = data.GetList<long>("production_time");
            ProductionResource = data.GetString("production_resource");
            ProductionAmount = data.GetInt("production_amount");
            MaxResource = data.GetInt("max_resource");
            ResourcesForWork = data.GetDictionary<string, int>("resources_for_work");
            ResourcesForProduction = data.GetDictionary<string, int>("resources_for_production");
        }
    }
}