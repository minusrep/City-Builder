using System;
using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Citizens;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class TimerActionDescription : ActionDescription
    {
        private const string TimerKey = "timer";
        
        private const string DurationKey = "duration";

        private string Timer { get; }

        private float Duration { get; }

        public TimerActionDescription(Dictionary<string, object> data)
        {
            Timer = data[TimerKey] as string;
            
            Duration = Convert.ToSingle(data[DurationKey]);
        }

        public override void Execute(World world, CitizenModel model)
        {
            model.Timers[Timer] = DateTimeOffset.UtcNow.AddSeconds(Duration).ToUnixTimeSeconds();
        }
    }
}