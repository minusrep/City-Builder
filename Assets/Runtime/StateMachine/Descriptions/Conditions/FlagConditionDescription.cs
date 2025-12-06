using System;
using System.Collections.Generic;

namespace Runtime.StateMachine
{
    public class FlagConditionDescription : ConditionDescription
    {
        private const string NameKey = "name";

        private const string StatusKey = "status";

        private string Name { get; set; }

        private bool Status { get; set; }

        public override bool Check(Dictionary<string, object> model)
        {
            var statusValue = Convert.ToBoolean(model[Name]);
            
            return statusValue == Status;
        }

        public override void Deserialize(Dictionary<string, object> data)
        {
            Name = data[NameKey] as string;
            Status = Convert.ToBoolean(data[StatusKey]);
        }
    }
}