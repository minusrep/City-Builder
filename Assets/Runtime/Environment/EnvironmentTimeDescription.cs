using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Environment
{
    public class EnvironmentTimeDescription
    {
        private const string CycleLengthKey = "cycle_length";
        private const string TimeScaleKey = "time_scale";
        
        public float CycleLength { get; private set; }
        
        public float TimeScale { get; private set; }

        public EnvironmentTimeDescription(Dictionary<string, object> data)
        {
            CycleLength = data.GetFloat(CycleLengthKey);
            TimeScale = data.GetFloat(TimeScaleKey);
        }        
    }
}