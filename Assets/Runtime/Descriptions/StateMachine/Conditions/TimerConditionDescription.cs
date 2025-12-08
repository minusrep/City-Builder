using System;
using System.Collections.Generic;

namespace Runtime.StateMachine.Descriptions.Conditions
{
    public class TimerConditionDescription : ConditionDescription
    {
        private const string NameKey = "name";
        private string Name { get; set; }

        public override bool Check(Dictionary<string, object> model)
        {
            if (!model.ContainsKey(NameKey)) return true;

            var endUnixSeconds = Convert.ToInt64(model[Name]);

            return DateTimeOffset.UtcNow.ToUnixTimeSeconds() >= (long)endUnixSeconds;
        }

        public override void Deserialize(Dictionary<string, object> data)
        {
            base.Deserialize(data);

            Name = data[NameKey] as string;
        }
    }
}