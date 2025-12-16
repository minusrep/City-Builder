using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Descriptions.Items
{
    public class ResourceDescription
    {
        public string Type { get; }
        public float ReductionTime { get; }
        public float ReductionAmount { get; }

        public ResourceDescription(Dictionary<string, object> data)
        {
            Type = data.GetString("type");
            ReductionTime = data.GetLong("reduction_time");
            ReductionAmount = data.GetFloat("reduction_amount");
        }
    }
}