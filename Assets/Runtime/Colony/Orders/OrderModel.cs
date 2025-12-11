using System.Collections.Generic;
using Runtime.Extensions;
using Runtime.ModelCollections;

namespace Runtime.Colony.Orders
{
    public class OrderModel : ISerializeModel, IDeserializeModel
    {
        public int Id { get; set; }
        public string FromBuildingId { get; }

        public OrderModel(int id, string fromBuildingId)
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