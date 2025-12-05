using System;
using System.Collections;
using System.Collections.Generic;
using Runtime.Colony.ModelCollections;
using Runtime.Descriptions.Orders;

namespace Runtime.Colony.Orders
{
    public abstract class OrderModel : ISerializeModel, IDeserializeModel
    {
        public int Id { get; set; }
        public List<int> CitizensId { get; set; }
        public int FromBuildingId { get; }
        public int ToBuildingId { get; set; }
        public OrderDescription Description { get; set; }
        public abstract string Type { get; }

        public OrderModel(int fromBuildingId)
        {
            FromBuildingId = fromBuildingId;
        }

        public Dictionary<string, object> Serialize() => new()
        {
            { "to_building_id", ToBuildingId },
            { "from_building_id", FromBuildingId },
            { "citizens_id", CitizensId },
            { "type", Type }
        };

        public void Deserialize(Dictionary<string, object> data)
        {
            var raw = data["citizens_id"] as IEnumerable ?? Array.Empty<object>();

            var citizensId = new List<int>();

            foreach (var id in raw)
            {
                citizensId.Add(Convert.ToInt32(id));
            }

            CitizensId = citizensId;
            ToBuildingId = Convert.ToInt32(data["to_building_id"]);
        }
    }
}