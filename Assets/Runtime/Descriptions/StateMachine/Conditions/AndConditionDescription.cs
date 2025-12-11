using System.Collections.Generic;
using Runtime.Colony;
using Runtime.StateMachine.Conditions;

namespace Runtime.Descriptions.StateMachine.Conditions
{
    public class AndConditionDescription : LogicConditionDescription
    {
        public AndConditionDescription(Dictionary<string, object> data) : base(data)
        {
        }

        public override bool Check(World world, IUserConditionModel user)
        {
            foreach (var condition in Conditions)
            {
                if (!condition.Check(world, user))
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}