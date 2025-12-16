using System;
using System.Collections.Generic;
using Runtime.Extensions;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class TimerActionDescription : ActionDescription
    {
        private const string TimerKey = "timer";
        
        private const string DurationKey = "duration";

        public string Timer { get; private set; }

        public float Duration { get; private set; }

        public TimerActionDescription(Dictionary<string, object> data) : base(data)
        {
            Timer = data[TimerKey] as string;
            
            Duration = Convert.ToSingle(data[DurationKey]);
        }
    }

    public class SearchBuildingDescription : ActionDescription
    {
        private const string ValueKey = "value";
        
        public string Value { get; private set; }
        
        public SearchBuildingDescription(Dictionary<string, object> data) : base(data)
        {
            Value = data.GetString(ValueKey);
        }
    }
}