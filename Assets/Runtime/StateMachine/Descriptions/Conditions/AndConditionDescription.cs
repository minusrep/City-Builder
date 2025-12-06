using System.Collections.Generic;

namespace Runtime.StateMachine
{
    public class AndConditionDescription : LogicConditionDescription
    {
        public override bool Check(Dictionary<string, object> model)
        {
            foreach (var condition in Conditions)
            {
                if (!condition.Check(model))
                {
                    return false;
                }
            }
            
            return true;
        }
    }
}