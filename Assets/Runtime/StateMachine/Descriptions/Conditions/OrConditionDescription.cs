using System.Collections.Generic;

namespace Runtime.StateMachine
{
    public class OrConditionDescription : LogicConditionDescription
    {
        public override bool Check(Dictionary<string, object> model)
        {
            foreach (var condition in Conditions)
            {
                if (condition.Check(model))
                {
                    return true;
                }
            }

            return false;
        }
    }
}