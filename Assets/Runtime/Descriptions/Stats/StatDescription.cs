using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Descriptions.Stats
{
    public class StatDescription
    {
        public string Id { get; }
        public float Min { get; }
        public float Max { get; }
        
        public StatDescription(string id, Dictionary<string, object> data)
        {
            Id = id;
            Min = data.GetFloat("min");
            Max = data.GetFloat("max");
        }
    }
}