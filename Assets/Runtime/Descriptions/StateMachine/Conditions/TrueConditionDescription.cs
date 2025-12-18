using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.StateMachine.Conditions;

namespace Runtime.Descriptions.StateMachine.Conditions
{
    public class TrueConditionDescription : ConditionDescription
    {
        public TrueConditionDescription(Dictionary<string, object> data) : base(data)
        {
            
        }

        public override bool Check(World world, IUserConditionModel user)
        {
            return true;
        }
    }
}