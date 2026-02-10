using System.Collections.Generic;
using System.Linq;

namespace Runtime.Descriptions.Achievements
{
    public class TriggerDescription
    {
        public string Key { get; }
        public string Value { get; }

        public TriggerDescription(Dictionary<string, object> data)
        {
            var firstPair = data.First();
            Key = firstPair.Key;
            Value = firstPair.Value.ToString();
        }
    }
}