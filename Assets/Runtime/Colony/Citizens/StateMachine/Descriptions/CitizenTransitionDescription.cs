using System;
using System.Collections.Generic;
using Runtime.Colony.Citizens.StateMachine.Descriptions.Conditions;

namespace Runtime.Colony.Citizens.StateMachine.Descriptions
{
    public class CitizenTransitionDescription : IDeserializeModel
    {
        private const string CompareTypeValue = "Compare";
        private const string FlagTypeValue = "Flag";
        
        private const string TypeKey = "type";
        private const string ToStateKey = "toState";
        private const string ConditionsKey = "conditions";

        public string ToState  { get; private set; }

        public List<CitizenTransitionConditionDescription> Conditions { get; private set; }

        public void Deserialize(Dictionary<string, object> data)
        {
            ToState = data[ToStateKey] as string;
            
            Conditions = new List<CitizenTransitionConditionDescription>();

            if (data[ConditionsKey] is not List<object> conditionObjects)
            {
                throw new Exception("Conditions list doesn't contain conditions");
            }
            
            foreach (var conditionObject in conditionObjects)
            {
                if (conditionObject is not Dictionary<string, object> condition)
                {
                    throw new Exception("Conditions list doesn't contain conditions");
                }
                
                var conditionType = condition[TypeKey] as string;
                
                CitizenTransitionConditionDescription newCondition = conditionType switch
                {
                    CompareTypeValue => new CitizenCompareConditionDescription(),
                    FlagTypeValue => new CitizenFlagConditionDescription(),
                    _ => throw new NotSupportedException("Unknown condition type")
                };
                
                newCondition.Deserialize(condition);
                
                Conditions.Add(newCondition);
            }
        }
    }
}