using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Descriptions.Items
{
    public class ResourceDescription
    {
        public string Id { get; }
        public string Type { get; }
        public float ReductionTime { get; }
        public float ReductionAmount { get; }
        public string ViewId { get; }

        public ResourceDescription(string id, Dictionary<string, object> data)
        {
            Id = id;
            Type = data.GetString("type");
            ReductionTime = data.GetLong("reduction_time");
            ReductionAmount = data.GetFloat("reduction_amount");
            ViewId = data.GetString("view_id");
        }
    }
}