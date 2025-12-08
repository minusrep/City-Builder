using System.Collections.Generic;
using Runtime.StateMachine.Descriptions.Extensions;

namespace Runtime.StateMachine.Descriptions.Conditions
{
    public abstract class LogicConditionDescription : ConditionDescription
    {
        private const string ConditionsKey = "conditions";
        protected List<ConditionDescription> Conditions => _conditions;

        private List<ConditionDescription> _conditions;

        public override void Deserialize(Dictionary<string, object> data)
        {
            base.Deserialize(data);

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