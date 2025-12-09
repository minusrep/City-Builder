using System;
using System.Collections.Generic;

namespace Runtime.StateMachine.Descriptions
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
}