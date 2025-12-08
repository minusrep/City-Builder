using System;
using System.Collections.Generic;

namespace Runtime.StateMachine.Descriptions.Actions
{
    public class TimerActionDescription : ActionDescription
    {
        private const string TimerKey = "timer";
        
        private const string DurationKey = "duration";
        
        public string Timer { get; private set; }
        
        public float Duration { get; private set; }
        
        public override void Deserialize(Dictionary<string, object> data)
        {
            base.Deserialize(data);
            
            Timer = data[TimerKey] as string;
            
            Duration = Convert.ToSingle(data[DurationKey]);
        }
    }
}