using System.Collections.Generic;

namespace Runtime.StateMachine.Descriptions.Conditions
{
    public class AndConditionDescription : LogicConditionDescription
    {
        public AndConditionDescription(Dictionary<string, object> data) : base(data)
        {
        }

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