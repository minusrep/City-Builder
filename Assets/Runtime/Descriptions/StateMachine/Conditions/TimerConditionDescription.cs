using System;
using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.StateMachine.Conditions;

namespace Runtime.Descriptions.StateMachine.Conditions
{
    public class TimerConditionDescription : ConditionDescription
    {
        private const string NameKey = "name";

        private string Name { get; set; }

        public TimerConditionDescription(Dictionary<string, object> data) : base(data)
        {
            Name = data[NameKey] as string;
        }

        public override bool Check(World world, IUserConditionModel user)
        {
            if (user is not ITimerModel timerModel)
            {
                return true;
            }

            if (!timerModel.Timers.TryGetValue(Name, out var endUnixSeconds))
            {
                return true;
            }

            return DateTimeOffset.UtcNow.ToUnixTimeSeconds() >= (long)endUnixSeconds;
        }
    }
}