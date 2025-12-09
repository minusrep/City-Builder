using System;
using System.Collections.Generic;

namespace Runtime.StateMachine.Descriptions.Conditions
{
    public class TimerConditionDescription : ConditionDescription
    {
        private const string NameKey = "name";

        private string Name { get; set; }

        public TimerConditionDescription(Dictionary<string, object> data) : base(data)
        {
            Name = data[NameKey] as string;
        }

        public override bool Check(Dictionary<string, object> model)
        {
            if (!model.ContainsKey(NameKey)) return true;

            var endUnixSeconds = Convert.ToInt64(model[Name]);

            return DateTimeOffset.UtcNow.ToUnixTimeSeconds() >= (long)endUnixSeconds;
        }
    }
}