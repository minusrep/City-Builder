using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Descriptions.Items
{
    public class ItemDescription
    {
        public string Type { get; }
        public float ReductionTime { get; }
        public float ReductionAmount { get; }

        public ItemDescription(Dictionary<string, object> data)
        {
            Type = data.GetString("type");
            ReductionTime = data.GetLong("reduction_time");
            ReductionAmount = data.GetFloat("reduction_amount");
        }
    }
}