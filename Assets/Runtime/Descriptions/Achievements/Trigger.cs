using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Descriptions.Achievements
{
    public class Trigger
    {
        public string Event { get; }
        public string Target { get; }

        public Trigger(Dictionary<string, object> data)
        {
            Event = data.GetString("event");
            Target = data.GetString("target");
        }
    }
}