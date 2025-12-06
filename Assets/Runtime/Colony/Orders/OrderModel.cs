using System.Collections.Generic;
using Runtime.Colony.ModelCollections;
using Runtime.Utilities;

namespace Runtime.Colony.Orders
{
    public class OrderModel : ISerializeModel, IDeserializeModel
    {
        public int Id { get; set; }
        public int FromBuildingId { get; }

        public OrderModel(int id, int fromBuildingId)
        {
            Id = id;
            FromBuildingId = fromBuildingId;
        }

        public Dictionary<string, object> Serialize() => new()
        {
            { "id", Id }
        };

        public void Deserialize(Dictionary<string, object> data)
        {
            Id = data.GetInt("id");
        }
    }
}