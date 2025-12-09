using System.Collections.Generic;

namespace Runtime.StateMachine.Descriptions.Conditions
{
    public abstract class LogicConditionDescription : ConditionDescription
    {
        private const string ConditionsKey = "conditions";

        protected List<ConditionDescription> Conditions => _conditions;

        private readonly List<ConditionDescription> _conditions;

        protected LogicConditionDescription(Dictionary<string, object> data) : base(data)
        {
            _conditions = new List<ConditionDescription>();

            if (data[ConditionsKey] is not List<Dictionary<string, object>> conditionsDictionary)
            {
                throw new System.Exception("Conditions dictionary expected");
            }
            
            foreach (var dictionary in conditionsDictionary)
            {
                _conditions.Add(dictionary.ToConditionDescription());
            }
        }
    }
}