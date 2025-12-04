using System.Collections.Generic;
using System;

namespace Runtime.Descriptions.GameResources
{
    public class ResourceDescription
    {
        public string Type { get; }
        public float ReductionTime { get; }
        public float ReductionAmount { get; }

        public ResourceDescription(Dictionary<string, object> data)
        {
            Type = (string)data["type"];
            ReductionTime = Convert.ToSingle(data["reduction_time"]);
            ReductionAmount = Convert.ToSingle(data["reduction_amount"]);
        }
    }
}