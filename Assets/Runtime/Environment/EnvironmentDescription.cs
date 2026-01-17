using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Environment
{
    public class EnvironmentDescription
    {
        private const string TimeKey = "time";
        public EnvironmentTimeDescription Time { get; private set; }

        public EnvironmentDescription(Dictionary<string, object> data)
        {
            Time = new EnvironmentTimeDescription(data.GetNode(TimeKey));
        }
    }
}