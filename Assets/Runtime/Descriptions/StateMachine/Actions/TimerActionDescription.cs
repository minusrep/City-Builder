using System;
using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.Citizens;
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

        public override void Execute(World world, CitizenModel model)
        {
            model.Timers[Timer] = DateTimeOffset.UtcNow.AddSeconds(Duration).ToUnixTimeSeconds();
        }
    }
}