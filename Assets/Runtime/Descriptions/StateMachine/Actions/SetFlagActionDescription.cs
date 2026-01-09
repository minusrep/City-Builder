using Runtime.Colony;
using Runtime.Colony.Citizens;
using Runtime.Descriptions.StateMachine.Actions;
using Runtime.Extensions;
using System;
using System.Collections.Generic;

namespace Runtime.Descriptions.StateMachine.Actions
{
    public class SetFlagActionDescription : ActionDescription
    {
        private const string FlagNameKey = "flag_name";
        private const string FlagValueKey = "flag_value";

        private readonly string _flagName;
        private readonly bool _flagValue;

        public SetFlagActionDescription(Dictionary<string, object> data) : base(data)
        {
            _flagName = data.GetString(FlagNameKey);
            _flagValue = data.GetBool(FlagValueKey);
        }

        public override void Execute(World world, CitizenModel model)
        {
            model.Flags[_flagName] = _flagValue;
        }
    }
}
