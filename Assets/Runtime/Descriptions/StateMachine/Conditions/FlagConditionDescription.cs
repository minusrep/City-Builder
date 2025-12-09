using System;
using System.Collections.Generic;

namespace Runtime.StateMachine.Descriptions.Conditions
{
    public class FlagConditionDescription : ConditionDescription
    {
        private const string FlagKey = "flag";

        private const string ValueKey = "value";
        
        private string Flag { get; set; }

        private bool Value { get; set; }
        
        public FlagConditionDescription(Dictionary<string, object> data) : base(data)
        {
            Flag = data[FlagKey] as string;
            Value = Convert.ToBoolean(data[ValueKey]);
        }

        public override bool Check(Dictionary<string, object> model)
        {
            var statusValue = Convert.ToBoolean(model[Flag]);
            
            return statusValue == Value;
        }
    }
}