using System.Collections.Generic;
using Runtime.Descriptions.Orders;

namespace Runtime.Colony.Orders
{
    public abstract class OrderModel: ISerializeModel, IDeserializeModel
    {
        public int Id { get; set; }
        public int ToBuildingId { get; set; }
        public int FromBuildingId { get; private set; }
        public List<int> CitizensId { get; set; }
        public ColonyOrderDescription Description { get; }

        public OrderModel(ColonyOrderDescription description, int fromBuildingId)
        {
            Description = description;
            FromBuildingId = fromBuildingId;
        }

        public Dictionary<string, object> Serialize() => new()
        {
            { "to_building_id", ToBuildingId },
            { "from_building_id", FromBuildingId },
            { "citizens_id", CitizensId },
        };

        public void Deserialize(Dictionary<string, object> data)
        {
            ToBuildingId = (int)data["to_building_id"];
            FromBuildingId = (int)data["from_building_id"];
            CitizensId = (List<int>)data["citizens_id"];
        }
    }
}