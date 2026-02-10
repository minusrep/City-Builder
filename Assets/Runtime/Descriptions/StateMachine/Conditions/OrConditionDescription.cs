using System.Collections.Generic;
using Runtime.Colony;
using Runtime.Colony.StateMachine.Conditions;

namespace Runtime.Descriptions.StateMachine.Conditions
{
    public class OrConditionDescription : LogicConditionDescription
    {
        public OrConditionDescription(Dictionary<string, object> data) : base(data)
        {
            
        }

        public override bool Check(World world, IUserConditionModel user)
        {
            foreach (var condition in Conditions)
            {
                if (condition.Check(world, user))
                {
                    return true;
                }
            }

            return false;
        }
    }
}