using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Descriptions.Achievements
{
    public class Trigger
    {
        public string Event { get; }
        public string Resource { get; }

        public Trigger(Dictionary<string, object> data)
        {
            Event = data.GetString("event");
            Resource = data.GetString("resource");
        }
    }
}