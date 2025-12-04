using System.Collections.Generic;
using Runtime.Utilities;

namespace Runtime.Descriptions.GameResources
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