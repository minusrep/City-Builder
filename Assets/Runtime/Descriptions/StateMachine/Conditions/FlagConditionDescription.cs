using System;
using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Core;

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

        public override bool Check(World world, IUserConditionModel user)
        {
            if (user is not IFlagsModel flagsModel) return false;
            
            return flagsModel.Flags[Flag] == Value;
        }
    }
}